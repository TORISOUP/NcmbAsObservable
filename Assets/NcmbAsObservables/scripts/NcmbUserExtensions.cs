using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCMB;
using UniRx;

namespace NcmbAsObservables
{
    /// <summary>
    /// NCMBUserの拡張
    /// </summary>
    public static class NcmbUserExtensions
    {

        /// <summary>
        /// 非同期処理で現在ログインしているユーザのauthDataの削除を行います。<br/>
        /// 通信結果が必要な場合はコールバックを指定するこちらを使用します。
        /// </summary>
        /// <param name="provider">SNS名</param>
        /// <returns>削除後のユーザ</returns>
        public static IObservable<NCMBUser> UnLinkWithAuthDataAsyncAsStream(this NCMBUser user, string provider)
        {
            return Observable.Create<NCMBUser>(observer =>
            {
                user.UnLinkWithAuthDataAsync(provider, error =>
                {
                    if (error == null)
                    {
                        observer.OnNext(user);
                        observer.OnCompleted();
                    }
                    else
                    {
                        observer.OnError(error);
                    }
                });
                return Disposable.Empty;
            });
        }

        /// <summary>
        /// 非同期処理で現在ログインしているユーザに、authDataの追加を行います。<br/>
        /// authDataが登録されていないユーザならログインし、authDataの登録を行います。<br/>
        /// authDataが登録されているユーザなら、authDataの追加を行います。<br/>
        /// 通信結果が必要な場合はコールバックを指定するこちらを使用します。
        /// </summary>
        /// <param name="linkParam">authData</param>
        /// <returns>登録したユーザ</returns>
        public static IObservable<NCMBUser> LinkWithAuthDataAsyncAsStream(this NCMBUser user, Dictionary<string, object> linkParam)
        {
            return Observable.Create<NCMBUser>(observer =>
            {
                user.LinkWithAuthDataAsync(linkParam, error =>
                 {
                     if (error == null)
                     {
                         observer.OnNext(user);
                         observer.OnCompleted();
                     }
                     else
                     {
                         observer.OnError(error);
                     }
                 });
                return Disposable.Empty;
            });
        }

        /// <summary>
        /// 非同期処理でauthDataを用いて、ユーザを登録します。<br/>
        /// 既存会員のauthData登録はLinkWithAuthDataAsyncメソッドをご利用下さい。<br/>
        /// 通信結果が必要な場合はコールバックを指定するこちらを使用します。
        /// </summary>
        /// <returns>登録したユーザ</returns>
        public static IObservable<NCMBUser> LogInWithAuthDataAsyncAsStream(this NCMBUser user)
        {
            return Observable.Create<NCMBUser>(observer =>
            {
                user.LogInWithAuthDataAsync(error =>
                {
                    if (error == null)
                    {
                        observer.OnNext(user);
                        observer.OnCompleted();
                    }
                    else
                    {
                        observer.OnError(error);
                    }
                });
                return Disposable.Empty;
            });
        }

        /// <summary>
        /// 非同期処理でユーザを登録します。<br/>
        /// オブジェクトIDが登録されていない新規会員ならログインし、登録を行います。<br/>
        /// オブジェクトIDが登録されている既存会員ならログインせず、更新を行います。<br/>
        /// 既存会員のログインはLogInAsyncメソッドをご利用下さい。<br/>
        /// 通信結果が必要な場合はコールバックを指定するこちらを使用します。
        /// </summary>
        /// <returns>登録したユーザ</returns>
        public static IObservable<NCMBUser> SingUpAsyncAsStream(this NCMBUser user)
        {
            return Observable.Create<NCMBUser>(observer =>
            {
                user.SignUpAsync(error =>
                {
                    if (error == null)
                    {
                        observer.OnNext(user);
                        observer.OnCompleted();
                    }
                    else
                    {
                        observer.OnError(error);
                    }
                });
                return Disposable.Empty;
            });
        }

    }
}
