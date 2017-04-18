using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCMB;
using UniRx;

namespace NcmbAsObservables
{
    /// <summary>
    /// NCMBUser関係の処理をObservableとして扱える
    /// </summary>
    public static class NcmbUserObservable
    {

        /// <summary>
        /// 非同期処理でメールアドレスとパスワードを指定して、ユーザのログインを行います。
        /// </summary>
        /// <returns>ログインしたユーザオブジェクト</returns>
        public static IObservable<NCMBUser> LogInWithMailAddressAsync(string email, string password)
        {
            return Observable.Create<NCMBUser>(observer =>
            {
                NCMBUser.LogInWithMailAddressAsync(email, password, error =>
                {
                    if (error == null)
                    {
                        observer.OnNext(NCMBUser.CurrentUser);
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
        /// 非同期処理でユーザ名とパスワードを指定して、ユーザのログインを行います。
        /// </summary>
        /// <returns>ログインしたユーザオブジェクト</returns>
        public static IObservable<NCMBUser> LogInAsync(string name, string password)
        {
            return Observable.Create<NCMBUser>(observer =>
            {
                NCMBUser.LogInAsync(name, password, error =>
                 {
                     if (error == null)
                     {
                         observer.OnNext(NCMBUser.CurrentUser);
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
        /// 非同期処理でユーザのパスワード再発行依頼を行います。
        /// </summary>
        public static IObservable<Unit> RequestPasswordResetAsync(string email)
        {
            return Observable.Create<Unit>(observer =>
            {
                NCMBUser.RequestPasswordResetAsync(email, error =>
                 {
                     if (error == null)
                     {
                         observer.OnNext(Unit.Default);
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
		/// 非同期処理で指定したメールアドレスに対して、<br/>
		/// 会員登録を行うためのメールを送信するよう要求します。<br/>
        /// </summary>
        public static IObservable<Unit> RequestAuthenticationMailAsync(string email)
        {
            return Observable.Create<Unit>(observer =>
            {
                NCMBUser.RequestAuthenticationMailAsync(email, error =>
                {
                    if (error == null)
                    {
                        observer.OnNext(Unit.Default);
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
        /// 非同期処理でユーザのログアウトを行います。<br/>
        /// </summary>
        public static IObservable<Unit> LogOutAsync()
        {
            return Observable.Create<Unit>(observer =>
            {
                NCMBUser.LogOutAsync(error =>
               {
                   if (error == null)
                   {
                       observer.OnNext(Unit.Default);
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
