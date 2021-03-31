using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace KAG.Menu
{
    public class AuthenticationMenu : MonoBehaviour
    {
        public Toast toast;

        public GameObject loginPanel;
        public TMP_InputField loginUsername;
        public TMP_InputField loginPassword;
        public Button loginButton;
        public Button guestButton;
        public Button goToRegisterButton;

        [Space]
        public GameObject registerPanel;
        public TMP_InputField registerUsername;
        public TMP_InputField registerEmail;
        public TMP_InputField registerPassword;
        public Button registerButton;
        public Button goToLoginButton;

        private void Start()
        {
            ShowLogin();
        }

        public void ShowLogin()
        {
            registerPanel.SetActive(false);
            loginPanel.SetActive(true);
        }

        public void ShowRegister()
        {
            loginPanel.SetActive(false);
            registerPanel.SetActive(true);
        }

        private void OnLoginSuccess(PlayerInfo player)
        {
            Debug.Log(player.Username);
            Debug.Log(player.UserId);

            SceneManager.LoadScene(GameScene.Menu);
        }

        private void OnLoginFailure(string error)
        {
            toast.Show("Error: " + error);
        }

        #region Dialog UI events
        public void OnLoginClicked()
        {
            toast.Show("Logging in...");

            GameSession.Instance.Login(loginUsername.text, loginPassword.text, OnLoginSuccess, OnLoginFailure);
        }

        public void OnRegisterClicked()
        {
            toast.Show("Registering...");

            GameSession.Instance.Register(registerUsername.text, registerEmail.text, registerPassword.text, OnLoginSuccess, OnLoginFailure);
        }

        public void OnGuestClicked()
        {
            toast.Show("Logging in as a guest...");

            GameSession.Instance.LoginGuest(OnLoginSuccess, OnLoginFailure);
        }

        public void OnOfflineClicked()
        {
            SceneManager.LoadScene(GameScene.Menu);
        }
        #endregion
    }
}
