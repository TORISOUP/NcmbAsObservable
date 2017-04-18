using NCMB;
using UniRx;

namespace NcmbAsObservables
{
    /// <summary>
    /// NCMBObjectの拡張
    /// </summary>
    public static class NcmbObjectExtensions
    {
        /// <summary>
        /// 非同期処理でオブジェクトの取得を行います。
        /// </summary>
        /// <returns>取得したオブジェクト</returns>
        public static IObservable<NCMBObject> FetchAsyncAsStream(this NCMBObject origin)
        {
            return Observable.Create<NCMBObject>(observer =>
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
        public static IObservable<NCMBObject> SaveAsyncAsStream(this NCMBObject origin)
        {
            return Observable.Create<NCMBObject>(observer =>
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
        public static IObservable<Unit> DeleteAsyncAsStream(this NCMBObject origin)
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
    }
}
