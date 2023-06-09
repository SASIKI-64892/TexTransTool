# AvatarDomainDefinition について

## このコンポーネントの概要

このコンポーネントは、各々の TextureTranformar が対象としているレンダラーのマテリアルしか変えれず、アバターとしての総マテリアル数(マテリアルスロット数ではない)やテクスチャーの枚数が増加してしまうのを防ぐためのコンポーネントです。

これは、これまで各々の対象のレンダラーまでしか影響できなかったものが他のレンダラーにも影響するようになるため、[TextureBlender](TextureBlender.md)などは大きな違いが出るようになります。

## 使い方

### 始めに

TexTransTool/Runtime にある AvatarMaterialDmain.cs から
ゲームオブジェクトに追加できます。

### MaterialDomainUse - Apply

このコンポーネントに表示されている、この Apply を使用すると、MaterialDmain を使用し、TextureTranformar がマテリアルを変更するとき、TextureTranformar が参照しているレンダラー以外のマテリアルも同時に変更されるようになります。

## プロパティ

### Avatar

アバターの範囲となる GameObject をセットできるプロパティです。

MaterialDomainUse - Apply を使用した場合、各々の TextureTranformer がマテリアルを変更したときの影響範囲がこの GameObject の配下のレンダラーになります。

### TexTransGrop

TexTransGrop の参照をセットできるプロパティです。
