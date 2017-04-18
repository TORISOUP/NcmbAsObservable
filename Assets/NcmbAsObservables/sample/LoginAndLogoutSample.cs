using NcmbAsObservables;
using System.Linq;
using UniRx;
using UnityEngine;

namespace NcmbAsObservables.Samples
{
    public class LoginAndLogoutSample : MonoBehaviour
    {
        private void Start()
        {
            ObservableFromNcmbUser
                .LogInAsync("test_user_name", "hogehoge") //Login
                .SelectMany(u => u.FetchAsyncAsStream())  //Fetch
                .Do(user => Debug.Log(string.Format("{0}\t{1}", user.UserName, user.Email))) //Show result
                .SelectMany(_ => ObservableFromNcmbUser.LogOutAsync()) //Log out
                .Subscribe(_ => Debug.Log("Logged out"), e => Debug.LogError(e));
        }
    }
}
