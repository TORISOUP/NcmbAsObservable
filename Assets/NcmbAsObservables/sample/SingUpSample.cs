using NCMB;
using UniRx;
using UnityEngine;

namespace NcmbAsObservables.sample
{
    public class SingUpSample : MonoBehaviour
    {
        void Start()
        {
            // Sing Up and Change Parameter and Fetch

            var user = new NCMBUser();

            user.UserName = "test_user_name";
            user.Password = "hogehoge";

            //Singup
            user.SingUpAsyncAsStream()
                .Catch((NCMBException e) =>
                {
                    Debug.LogError("Error on sing up:" + e);
                    return Observable.Empty<NCMBUser>();
                })
                .SelectMany(u =>
                {
                    //Change email and Age column when signed up
                    u.Email = "test@test.com";
                    u["Age"] = 20;
                    return u.SaveAsyncAsStream(); //Save
                })
                .Catch((NCMBException e) =>
                {
                    Debug.LogError("Error on save:" + e);
                    return Observable.Empty<NCMBUser>();
                })
                .SelectMany(u => u.FetchAsyncAsStream()) //Fetch
                .Catch((NCMBException e) =>
                {
                    Debug.LogError("Error on Fetch:" + e);
                    return Observable.Empty<NCMBUser>();
                })
                .Subscribe(u =>
                {
                    Debug.Log(string.Format("{0}\t{1}\t{2}", u.UserName, u.Email, u["Age"]));
                }, e =>
                {
                    Debug.LogError("Unknown Error:" + e);
                });
        }

    }
}
