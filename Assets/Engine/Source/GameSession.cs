using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using Mirror;
using UnityEngine;

namespace KAG
{
    public static class GameScene
    {
        public const string Authentication = "Authentication";
        public const string Matchmaking = "Matchmaking";
        public const string Match = "Match";
        public const string Menu = "Menu";
    }

    public struct PlayerInfo : NetworkMessage
    {
        public string Username;
        public string UserId;
    }

    public struct ServerInfo
    {
        public string Name;
        public string IP;
    }

    public class GameSession
    {
        public enum SessionType {
            Solo,
            Client,
            Server
        }

        public SessionType type;

        public static GameSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameSession();
                }

                return _instance;
            }
        }
        private static GameSession _instance;

        public PlayerInfo player = new PlayerInfo()
        {
            Username = "Peasant"
        };

        #region Session events
        public delegate void OnLoginSuccess(PlayerInfo player);
        public delegate void OnLoginFailure(string error);
        public delegate void OnLogout();

        public delegate void OnMatchmakeRefresh(List<ServerInfo> serverList);
        #endregion

        public void LoginGuest(OnLoginSuccess onSuccess, OnLoginFailure onFailure)
        {
#if UNITY_ANDROID
            var request = new LoginWithAndroidDeviceIDRequest {
                CreateAccount = true,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
                    GetUserAccountInfo = true
                }
            };

            PlayFabClientAPI.LoginWithAndroidDeviceID(request,
            result =>
            {
                player.Username = "Guest-" + result.PlayFabId;
                player.UserId = result.PlayFabId;
                onSuccess.Invoke(player);
            },
            error =>
            {
                onFailure.Invoke(error.GenerateErrorReport());
            });
#elif UNITY_IOS
            var request = new LoginWithIOSDeviceIDRequest {
                CreateAccount = true,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
                    GetUserAccountInfo = true
                }
            };

            PlayFabClientAPI.LoginWithIOSDeviceID(request,
            result =>
            {
                player.Username = "Guest-" + result.PlayFabId;
                player.UserId = result.PlayFabId;
                onSuccess.Invoke(player);
            },
            error =>
            {
                onFailure.Invoke(error.GenerateErrorReport());
            });
#else
            string guestID = PlayerPrefs.GetString("GuestID");
            if (string.IsNullOrWhiteSpace(guestID))
            {
                guestID = "";
                for (var i = 0; i < 16; i++)
                {
                    guestID += ((char)('A' + UnityEngine.Random.Range(0, 26))).ToString();
                }
                PlayerPrefs.SetString("GuestID", guestID);
            }

            var request = new LoginWithCustomIDRequest
            {
                CreateAccount = true,
                CustomId = guestID,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetUserAccountInfo = true
                }
            };

            PlayFabClientAPI.LoginWithCustomID(request,
            result =>
            {
                player.Username = "Guest_" + result.PlayFabId;
                player.UserId = result.PlayFabId;
                onSuccess.Invoke(player);
            },
            error =>
            {
                onFailure.Invoke(error.GenerateErrorReport());
            });
#endif
        }

        public void Login(string username, string password, OnLoginSuccess onSuccess, OnLoginFailure onFailure)
        {
            var request = new LoginWithPlayFabRequest
            {
                Username = username,
                Password = password,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {
                    GetUserAccountInfo = true
                }
        };

            PlayFabClientAPI.LoginWithPlayFab(request, 
            result =>
            {
                player.Username = result.InfoResultPayload.AccountInfo.Username;
                player.UserId = result.PlayFabId;
                onSuccess.Invoke(player);
            }, 
            error =>
            {
                onFailure.Invoke(error.GenerateErrorReport());
            });
        }

        public void Register(string username, string email, string password, OnLoginSuccess onSuccess, OnLoginFailure onFailure)
        {
            var request = new RegisterPlayFabUserRequest
            {
                Username = username,
                Email = email,
                Password = password,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetUserAccountInfo = true
                }
            };

            PlayFabClientAPI.RegisterPlayFabUser(request,
            result =>
            {
                player.Username = result.Username;
                player.UserId = result.PlayFabId;
                onSuccess.Invoke(player);
            },
            error =>
            {
                onFailure.Invoke(error.GenerateErrorReport());
            });
        }

        public void Logout(OnLogout action) {
            action.Invoke();
        }
    }
}
