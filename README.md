# NcmbAsObservable

NcmbAsObservableは、NIFTYCloudが提供している[NCMB Unityプラグイン](https://github.com/NIFTYCloud-mbaas/ncmb_unity)をUniRxを用いて扱いやすくするライブラリです。

# 導入方法

[こちら](https://github.com/TORISOUP/NcmbAsObservable/releases)からunitypackageをダウンロードしてください。


# 使い方

まずは `NcmbAsObservables` へのusingを追加してください。

## 拡張メソッド

末尾がAsyncと表記された非同期APIを **AsyncAsStream と呼びかえることで`IObservable`として扱うことができるようになります。

```csharp
var user = new NCMBUser();

user.UserName = "test_user_name";
user.Password = "hogehoge";

//Singup
user.SingUpAsyncAsStream()
    .Subscribe(u =>
    {
        Debug.Log(string.Format("{0}", u.UserName));
    }, e =>
    {
        Debug.LogError("Unknown Error:" + e);
    });
```

## NCMBQuery

NCMBQueryは`NCMBQueryHelper<T>`を利用することで`IObservable`で扱えるようになります。

```csharp
var query = new NCMBQuery<NCMBObject>("Score");
query.OrderByDescending("score");
query.Limit = 5;

NcmbQueryHelper<NCMBObject>
    .FindAsync(query)
    .Subscribe(resultList =>
    {
        foreach (var o in resultList)
        {
            Debug.Log(o);
        }
    }, error => Debug.LogError(error));
```

## NCMBUser

NcmbUserにstaticで実装されているAsync系のAPIは`ObservableFromNcmbUser`から呼び出すことができます。

```csharp
ObservableFromNcmbUser
    .LogInAsync("test_user_name", "hogehoge") //Login
    .SelectMany(u => u.FetchAsyncAsStream())  //Fetch
    .Do(user => Debug.Log(string.Format("{0}\t{1}", user.UserName, user.Email))) //Show result
    .SelectMany(_ => ObservableFromNcmbUser.LogOutAsync()) //Log out
    .Subscribe(_ => Debug.Log("Logged out"), e => Debug.LogError(e));
```

# 配布ライセンス

MIT License

# 使用ライブラリ

PhotonRxはUniRxをベースに作成しています
Copyright (c) 2014 Yoshifumi Kawai https://github.com/neuecc/UniRx/blob/master/LICENSE
