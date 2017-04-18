using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCMB;
using UniRx;

namespace NcmbAsObservables
{
    /// <summary>
    /// NcmbQueryの実行結果をObservableとして提供する
    /// </summary>
    public struct NcmbQueryHelper<T> where T : NCMBObject
    {
        /// <summary>
        /// クエリにマッチするオブジェクトを取得を行います。
        /// </summary>
        /// <returns>結果</returns>
        public static IObservable<List<T>> FindAsync(NCMBQuery<T> query)
        {
            return Observable.Create<List<T>>(observer =>
            {
                query.FindAsync((objects, error) =>
                {
                    if (error == null)
                    {
                        observer.OnNext(objects);
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
        /// 指定IDのオブジェクトを取得を行います。
        /// </summary>
        /// <returns>結果</returns>
        public static IObservable<T> GetAsync(NCMBQuery<T> query, string objectId)
        {
            return Observable.Create<T>(observer =>
            {
                query.GetAsync(objectId, (objects, error) =>
                {
                    if (error == null)
                    {
                        observer.OnNext(objects);
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
        ///クエリにマッチするオブジェクト数の取得を行います。
        /// </summary>
        /// <returns>カウント数</returns>
        public static IObservable<int> CountAsync(NCMBQuery<T> query)
        {
            return Observable.Create<int>(observer =>
            {
                query.CountAsync((count, error) =>
                {
                    if (error == null)
                    {
                        observer.OnNext(count);
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
