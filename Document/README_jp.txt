このアセットはacchub.netを通して、ユーザのスマホの振り運動を取得するサンプルです。
acchub.netの詳細はこちら -> http://acchub.net


・事前準備
メニューの「Edit」 -> 「Project Settings」 -> 「Other Settings」-> 「Scripting Runtime Version」をExperimental(.NET 4.6 Equivelent)に設定して下さい。

・はじめる（初級）
(1) acchub/Prefab/acchub_http_get_all
	定期的にacchub.netから振り運動データを取得します。
	InspectorのAcchub_http_get_all(Script)のTopic_に、設定したいトピック文字列を入力します。
	（デフォルトのdebug/randomはサーバが疑似ユーザを生成しているトピックです。0人～100人の間をユーザ数が増減します。）
	
	InspectorのAcchub_object_controller(Script)のPref_client_に、ユーザ１人に相当するGameObjectを設定します。
	サンプルシーンではacchub/Prefab/Sphereがセットされています。

(2) acchub/Prefab/Sphere
	ユーザ１人に相当するアバターのサンプル（ただの球）です。ユーザがスマホで接続すると生成され、切断すると破棄されます。
	ユーザがスマホを振ると、その周期に合わせて球が伸び縮みして動きます。
	このGameObjectにはacchub_client_dataとacchub_characterがセットされている必要があります。	このオブジェクトを(1)のPref_client_に設定することで、ユーザ接続時に生成されます。
	
(3) シーンを実行

(4) スマホ・accaccの操作
	スマホにaccaccをインストール（http://acchub.net）・起動
	(1)で設定したトピックを入力、[Connect]ボタンを押してacchub.netに接続する
	スマホを振る

・カスタマイズ（上級）
下記の２クラスのソースコードを改造することで、ユーザアバターの動きや、出現位置などを変更することができます。
	acchub_object_controller.cs ユーザアバターの生成を行う。
	acchub_character.cs スマホの振り運動データを取得し、オブジェクトを動かす。
	
	ユーザアバターの動きは、基本的に下記２変数で制御することになります。
		acchub_client_data.normalized_value_
		acchub_client_data.power_current_
	normalized_value_は -1.0f～1.0fの周期的な値（正弦波）を返します。
	ユーザがスマホを素早く往復させれば、この周期が短くなります。
	ユーザのスマホの振り幅（加速度の大きさ）は、power_current_で取得できます。
	