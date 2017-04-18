using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCMB;
using UniRx;

namespace Assets.NcmbAsObservables
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
                origin.SendPush(error =>
                {
                    if (error == null)
                    {
                        observer.OnNext(origin);
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
