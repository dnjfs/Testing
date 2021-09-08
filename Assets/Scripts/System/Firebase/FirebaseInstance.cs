using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Auth;

public class FirebaseInstance : MonoBehaviour
{
    //FirebaseApp app = null;
    FirebaseAuth auth;
    FirebaseUser user;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); //���̾�̽� ���� ���¸� �����ϱ� ���� �������� ����
        //Login ���� ���� ���� ó������ �� �� �� ����ǹǷ� �̱��� ó���� ���� �ʾ���
    }

    void Start()
    {
        //���̾�̽� ����
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                //app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                LoginAnonymous();
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    void LoginAnonymous()
    {
        auth = FirebaseAuth.DefaultInstance;
        //�͸� �α���
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                //���� ���� �� ȭ�鿡 ���� �޽��� ���
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                //���� ���� �� ȭ�鿡 ���� �޽��� ���
                return;
            }

            user = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                user.DisplayName, user.UserId);
        });
    }

    void OnDestroy()
    {
        if (user != null)
        {
            user.DeleteAsync().ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("DeleteAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("DeleteAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User deleted successfully.");
            });
        }
    }
}
