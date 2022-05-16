Unity Version: `2021 LTS`
Author: `James McGhee`

# Bootstrap Scene Example
This example demonstrates the use of a bootstrap scene and gives examples of managing scene loading using both the Additive method (recomended) and the Do Not Destroy On Load Method (not-recomended).

# Instructions
Located in the project you will find the following folders

* Assets\Additive Example
* Assets\DNDoL Example

Each example contains its own set of scenes and scripts.

Please note that the scripts load scenes based on build index, thus it is important that you configure your project's Build Settings build indexs appropreatly for the examples your running. 

For example to run the Additive Example you would set the following scenes build index up

`0 Assets\Additive Example\bootstrap.unity`
`1 Assets\Additive Example\title.unity`
`2 Assets\Additive Example\gameplay.unity`

in contrast you for the DNDoL Example you would set

`0 Assets\DNDoL Example\bootstrap.unity`
`1 Assets\DNDoL Example\title.unity`
`2 Assets\DNDoL Example\gameplay.unity`

