==========================================================
	Quine–McCluskey 
	論理回路簡単化ソフトウェア
==========================================================

- 概要 ---------------------------------------------------
	このソフトウェアは、加法標準形、真理値表から、
	クワインマキラスキー法を用いて、
	論理回路を簡単化できるソフトウェアです。

- 説明 ---------------------------------------------------

　①起動
	このファイルが入っている同じ階層に
	「Quine–McCluskey.exe」という実行ファイルがあります。
	それをダブルクリックで起動できます。

　②初期設定
	まず、簡単化したい論理式の入力端子(要素)数を
	テキストボックスに入力してください。
	（最初は5が入力されています。）
	入力したら隣の適用ボタンをクリックすることで
	入力パネルと真理値表が準備されます。
	※入力途中に適用をクリックすると
	　入力情報は消えてしまいます

　③論理式情報入力
	論理式の指定方法には2種類あります。

	☆加法標準形で指定
	　　・入力パネル内の各ボタン
	　　	白→緑→赤と変化し、
		パネル左下の情報も変わります
	　　	それぞれ　白＝含めない　緑＝1 ON　赤=0 OFF
	　　	となっており、項1つ分の入力を表現できます。

	　　・Addボタン
		現在入力している項を、加法標準形の式に
		加えることができます。
		加えたら入力している項を切り替えても
		加法標準形の式は保持されます。

	　　・Clearボタン
		加法標準形の式を消すことができます。
		※全消去 

	☆真理値表で指定
	　　真理値表の場合は、OUT列の数字をクリック
	　　することで、出力を０と１で切り替えられます。

　④計算
	「加法標準形から導出」ボタン
		加法標準形の式を簡単化します。
		※真理値表は上書きされます。

	「真理値表から導出」ボタン
		真理値表から式の簡単化を行います
		※加法標準形の入力情報は保持されます

	簡単化した式は、結果のテキストブロックに表示されます

-その他----------------------------------------------------
うまくいかないときは、ウインドウ下のエラー表示を確認してください。