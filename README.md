# DapperCastErrorSample

このサンプルは、Dapper+SQLiteで検索した際に、double?型への変換に失敗した時の対処法を記載した物です。  
以下のURLで触れられています。 
https://qrunch.net/@maccyo/entries/toOHtxzaj3Dy4QEp 
https://github.com/StackExchange/Dapper/issues/642#issuecomment-435277926  
自動的な型変換がうまく行かない時には大体この方法でうまくいくはずです。 
以下の行をコメントアウトしてあります。 
'''Program.cs
SqlMapper.AddTypeHandler(typeof(System.Double),new DoubleConverter());
'''

このまま実行すると、以下のような例外が発生します。 
Unhandled Exception: System.Data.DataException: Error parsing column 1 (Column2=36 - Double) ---> System.InvalidCastException: Unable to cast object of type 'System.Double' to type 'System.Int32'.  

コメントを解除すれば、正常に実行できるはずです。
