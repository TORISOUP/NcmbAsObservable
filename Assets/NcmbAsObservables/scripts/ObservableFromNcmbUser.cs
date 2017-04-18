using NCMB;
using UniRx;

namespace NcmbAsObservables
{
    /// <summary>
    /// NCMBUser関係の処理をObservableとして扱える
    /// </summary>
    public static class ObservableFromNcmbUser
    {
        /// <summary>
        /// 非同期処理でメールアドレスとパスワードを指定して、ユーザのログインを行います。
        /// </summary>
        /// <returns>ログインしたユーザオブジェクト</returns>
        public static IObservable<NCMBUser> LogInWithMailAddressAsync(string email, string password)
        {
            return Observable.Create<NCMBUser>(observer =>
            {
                var gate = new object();
                var isDisposed = false;
                NCMBUser.LogInWithMailAddressAsync(email, password, error =>
                {
                    lock (gate)
                    {
                        if (isDisposed) return;
                        if (error == null)
                        {
                            observer.OnNext(NCMBUser.CurrentUser);
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
        /// 非同期処理でユーザ名とパスワードを指定して、ユーザのログインを行います。
        /// </summary>
        /// <returns>ログインしたユーザオブジェクト</returns>
        public static IObservable<NCMBUser> LogInAsync(string name, string password)
        {
            return Observable.Create<NCMBUser>(observer =>
            {
                var gate = new object();
                var isDisposed = false;
                NCMBUser.LogInAsync(name, password, error =>
                {
                    lock (gate)
                    {
                        if (isDisposed) return;
                        if (error == null)
                        {
                            observer.OnNext(NCMBUser.CurrentUser);
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
        /// 非同期処理でユーザのパスワード再発行依頼を行います。
        /// </summary>
        public static IObservable<Unit> RequestPasswordResetAsync(string email)
        {
            return Observable.Create<Unit>(observer =>
            {
                var gate = new object();
                var isDisposed = false;
                NCMBUser.RequestPasswordResetAsync(email, error =>
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
		/// 非同期処理で指定したメールアドレスに対して、<br/>
		/// 会員登録を行うためのメールを送信するよう要求します。<br/>
        /// </summary>
        public static IObservable<Unit> RequestAuthenticationMailAsync(string email)
        {
            return Observable.Create<Unit>(observer =>
            {
                var gate = new object();
                var isDisposed = false;
                NCMBUser.RequestAuthenticationMailAsync(email, error =>
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
        /// 非同期処理でユーザのログアウトを行います。<br/>
        /// </summary>
        public static IObservable<Unit> LogOutAsync()
        {
            return Observable.Create<Unit>(observer =>
            {
                var gate = new object();
                var isDisposed = false;
                NCMBUser.LogOutAsync(error =>
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
    }
}
