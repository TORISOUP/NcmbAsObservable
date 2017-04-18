using NCMB;
using UniRx;

namespace NcmbAsObservables
{
    public static class NcmbPushExtensions
    {
        /// <summary>
        /// プッシュの送信を行います。
        /// </summary>
        public static IObservable<NCMBPush> SendPushAsync(this NCMBPush origin)
        {
            return Observable.Create<NCMBPush>(observer =>
            {
                var gate = new object();
                var isDisposed = false;
                origin.SendPush(error =>
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
    }
}
