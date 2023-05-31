# SimpleDecal について

## このコンポーネントの概要

このコンポーネントは、非破壊的に紋章やマークをメッシュに対して貼り付けることを目的とした物で、貼り付けるテクスチャー次第で幅広いことができるコンポーネントです。

## 使い方

### 始めに

TexTransTool/Runtime/Decal にある SimpleDecalAvatarTag.cs から、
またはインスペクターのコンポーネントを追加の TexTransTool/SimpleDecal から
ゲームオブジェクトに追加できます。

### デカールの張り方

 - 適当なGameObjectに上記の方法でコンポーネントを付け
 - デカールを張りたいメッシュを持つレンダラーをTargetRendererにセット
 - DecalTextureに張りたいテクスチャーをセット
 - GameObjectの位置、Scale、MaxDistansなどを調整し
 - Compileボタンを押し、貼り付けられたテクスチャを生成

Appry ボタンを押すとそのデカールをプレビューすることができます。

## プロパティ

### TargetRenderer

ターゲットとなるレンダラーをセットするプロパティ。

### DecalTexture

貼り付けるデカールをセットするプロパティ。

### Scale

貼り付けるデカールのサイズを調整できるプロパティで、そのゲームオブジェクトのトランスフォーㇺのScaleのX,Yを変更します。

### MaxDistans

貼り付けるデカールの最大奥行きを調整できるプロパティで、そのゲームオブジェクトのトランスフォームのScaleのZを変更します。
頬などに張り付ける場合白目のメッシュに誤って張られてしまうのを防止できます。

### AdvansdMode

詳細な設定を行えるモードのチェックです。

これより下はAdvansdModeにチェックが入っている場合に表示されるプロパティなどになります。

### TargetRenderer(配列)

複数のレンダラーに跨るデカールを張るとき、枠を+ボタンで増やし、セットできます。

### FicedAspect

これのチェックが入っている場合、サイズの値が一つのfloatとなり、画像のアスペクト比と同じ比率に、デカールを張り付ける範囲が調整されます。

これのチェックが外れている場合、サイズの値が二つのfloatとなり、画像のアスペクト比を無視し、Xを幅、Yを高さとして、デカールを張り付ける範囲が調整できます。

主に髪の毛にグラデーションを入れるときなどに使用します。

AdvansdModeにチェックが入っていない場合このプロパティはチェックが入っている状態になります。

### BlendType

デカールを元画像と合成するときの合成モードを選択するプロパティです。[詳細](BlendType.md)

AdvansdModeにチェックが入っていない場合このプロパティはNormalになります。

### TargetPropatyName

デカールを張るテクスチャーをマテリアルのどのプロパティのテクスチャーにするかを選択するプロパティです。

### PoligonCaling

polygonをカリングする条件を調整できるプロパティです。
特に理由がない場合Vartexをお勧めします。

 - Vartex 頂点ベースでカリングします。
 - Edge 辺ベースでカリングします。デカールを張る範囲に頂点が入らない場合に。
 - EdgeAndCenterRey 辺ベースのカリングに加え中央から疑似的なレイキャストを行いカリングします。辺が一つも入らないほどポリゴンに比べてデカールを張り付ける範囲が小さい場合に。

AdvansdModeにチェックが入っていない場合このプロパティはVartexになります。

### SideChek
チェックが入っている場合、デカールを張る範囲に対して裏面である場合そのポリゴンがカリングされ、デカールが張られなくなります。

チェックが入っていない場合、裏面にもデカールが張られます。

髪の毛に対してグラデーションを入れるときチェックを外すと良いです。

AdvansdModeにチェックが入っていない場合このプロパティはチェックが入っている状態になります。