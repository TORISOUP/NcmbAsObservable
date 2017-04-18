using NCMB;
using System.Collections.Generic;
using UniRx;

namespace NcmbAsObservables
{
    /// <summary>
    /// NCMBUserの拡張
    /// </summary>
    public static class NcmbUserExtensions
    {
        /// <summary>
        /// 非同期処理でオブジェクトの取得を行います。
        /// </summary>
        /// <returns>取得したオブジェクト</returns>
        public static IObservable<NCMBUser> FetchAsyncAsStream(this NCMBUser origin)
        {
            return Observable.Create<NCMBUser>(observer =>
            {
                var gate = new object();
                var isDisposed = false;
                origin.FetchAsync(error =>
                {
                    lock (gate)
                    {
                        if (isDisposed) return;

                        if (error == null)
                        {
                            observer.OnNext(origin);
                            observer.OnCompleted();
                        }
                        else
                        {
                            observer.OnError(error);
                        }
                    }
                });
                return Disposable.Create(() =>
                {
                    lock (gate)
                    {
                        isDisposed = true;
                    }
                });
            });
        }

        /// <summary>
		/// 非同期処理でオブジェクトの保存を行います。<br/>
		/// SaveAsync()を実行してから編集などをしていなく、保存をする必要が無い場合は通信を行いません。<br/>
		/// オブジェクトIDが登録されていない新規オブジェクトなら登録を行います。<br/>
		/// オブジェクトIDが登録されている既存オブジェクトなら更新を行います。<br/>
        /// </summary>
        /// <returns>もとのオブジェクト</returns>
        public static IObservable<NCMBUser> SaveAsyncAsStream(this NCMBUser origin)
        {
            return Observable.Create<NCMBUser>(observer =>
            {
                var gate = new object();
                var isDisposed = false;
                origin.SaveAsync(error =>
                {
                    lock (gate)
                    {
                        if (isDisposed) return;
                        if (error == null)
                        {
                            observer.OnNext(origin);
                            observer.OnCompleted();
                        }
                        else
                        {
                            observer.OnError(error);
                        }
                    }
                });
                return Disposable.Create(() =>
                {
                    lock (gate)
                    {
                        isDisposed = true;
                    }
                });
            });
        }

        /// <summary>
        /// オブジェクトの削除を行います。
        /// </summary>
        public static IObservable<Unit> DeleteAsyncAsStream(this NCMBUser origin)
        {
            return Observable.Create<Unit>(observer =>
            {
                var gate = new object();
                var isDisposed = false;
                origin.DeleteAsync(error =>
                {
                    lock (gate)
                    {
                        if (isDisposed) return;
                        if (error == null)
                        {
                            observer.OnNext(Unit.Default);
                            observer.OnCompleted();
                        }
                        else
                        {
                            observer.OnError(error);
                        }
                    }
                });
                return Disposable.Create(() =>
                {
                    lock (gate)
                    {
                        isDisposed = true;
                    }
                });
            });
        }

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
                var gate = new object();
                var isDisposed = false;
                user.UnLinkWithAuthDataAsync(provider, error =>
                {
                    lock (gate)
                    {
                        if (isDisposed) return;
                        if (error == null)
                        {
                            observer.OnNext(user);
                            observer.OnCompleted();
                        }
                        else
                        {
                            observer.OnError(error);
                        }
                    }
                });
                return Disposable.Create(() =>
                {
                    lock (gate)
                    {
                        isDisposed = true;
                    }
                });
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
                var gate = new object();
                var isDisposed = false;
                user.LinkWithAuthDataAsync(linkParam, error =>
                {
                    lock (gate)
                    {
                        if (isDisposed) return;
                        if (error == null)
                        {
                            observer.OnNext(user);
                            observer.OnCompleted();
                        }
                        else
                        {
                            observer.OnError(error);
                        }
                    }
                });
                return Disposable.Create(() =>
                {
                    lock (gate)
                    {
                        isDisposed = true;
                    }
                });
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
                var gate = new object();
                var isDisposed = false;
                user.LogInWithAuthDataAsync(error =>
                {
                    lock (gate)
                    {
                        if (isDisposed) return;
                        if (error == null)
                        {
                            observer.OnNext(user);
                            observer.OnCompleted();
                        }
                        else
                        {
                            observer.OnError(error);
                        }
                    }
                });
                return Disposable.Create(() =>
                {
                    lock (gate)
                    {
                        isDisposed = true;
                    }
                });
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
                var gate = new object();
                var isDisposed = false;
                user.SignUpAsync(error =>
                {
                    lock (gate)
                    {
                        if (isDisposed) return;
                        if (error == null)
                        {
                            observer.OnNext(user);
                            observer.OnCompleted();
                        }
                        else
                        {
                            observer.OnError(error);
                        }
                    }
                });
                return Disposable.Create(() =>
                {
                    lock (gate)
                    {
                        isDisposed = true;
                    }
                });
            });
        }
    }
}
