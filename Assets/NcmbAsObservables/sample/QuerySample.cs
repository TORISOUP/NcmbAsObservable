using NCMB;
using UniRx;
using UnityEngine;

namespace NcmbAsObservables.Samples
{
    /// <summary>
    /// 参考： https://github.com/NIFTYCloud-mbaas/NCMB2dst_comp
    /// </summary>
    public class QuerySample : MonoBehaviour
    {
        private void Start()
        {
            var query = new NCMBQuery<NCMBObject>("Score");
            query.OrderByDescending("score");
            query.Limit = 5;

            query
                .FindAsyncAsStream()
                .Subscribe(resultList =>
                {
                    foreach (var o in resultList)
                    {
                        Debug.Log(o);
                    }
                }, error => Debug.LogError(error));
        }
    }
}
