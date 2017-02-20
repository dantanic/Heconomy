<img src="http://i.imgur.com/cU6s6a9.jpg"/>

# Welcome to HerbPlugins Repository

This organization is MiNET plugins repository I made, and you can download it.

# Overview

### Q. What is this?

An advanced economy plugin for MiNET.
<br>
<br>
You can manage your server economy with Heconomy.
<br>
With this plugin, you can using many features, seeing your money, pay money for other player... etc

### Q. What I have to prepare?

You must installed MiNET v1.0.1322 and .NET Framework 4.6.1.

### Q. How to using commands?

> | Command | Description | Permission | Arguments |
> | :-------: | :-------: | :-------: | :-------: |
> | `/money` | Shows player money amount or you. | default |  |
> | `/pay` | Pays money to player. | default | `<player: string> <amount: int>` |  |

### Q. Is there any permissions?

<dd><i><b>heconomyapi.*</b> - HeconomyAPI permissions.</i></dd>
<dd><i><b>heconomyapi.command.money</b> - HeconomyAPI command Money permission.</i></dd>
<dd><i><b>heconomyapi.command.pay</b> - HeconomyAPI command Pay permission.</i></dd>

### Q. How to manage player's money data?

<dd><i><b>example.json (example: Player's name)</b></i><dd>

```json
{
   "Money" : 10
}
```

You can manage player's money data using **Money** section.

### Q. Can I see screenshots?

<img src="http://i.imgur.com/mMcVJJQ.jpg"/>
<br>
<img src="http://i.imgur.com/4mcjFmc.jpg"/>

### Q. Is there provides api functions? 

We provides HeconomyAPI api functions for developers, read this step and try to using api functions.
<br>
<br>
<bb><i><b>Steps for registering HeconomyAPI api.</b></i><dd>

*I. Download HeconomyAPI plugin.*
<br>
<br>
*II. Include HeconomyAPI api in your C# code.*

```c#
using HeconomyAPI;
```

*III. Call the api functions.*

```c#
HeconomyAPI.GetAPI().function;
```

*Example Code.*

```c#        
[Command]        
public void rich(Player sender)        
{            
   if (HeconomyAPI.GetAPI().GetMoney(sender.Username) > 10000)
   {
      sender.SendMessage("You are rich!");   
   }
}
```

<bb><i><b>API functions.</b></i><dd>

###### Get Money Symbol
```c#
public string GetMoneySymbol()
{

}
```
<bb><i><b>Description: </b>Gets money symbol.</i><dd>
<bb><i><b>Returns: </b>money symbol</i><dd>

###### Get Default Money
```c#
public int GetDefaultMoney()
{

}
```
<bb><i><b>Description: </b>Gets default money amount.</i><dd>
<bb><i><b>Returns: </b>default money amount</i><dd>

###### Get Minimum Money
```c#
public int GetMinimumMoney()
{

}
```
<bb><i><b>Descriptions: </b>Gets minumum money amount.</i><dd>
<bb><i><b>Returns: </b>minimum money amount</i><dd>

###### Get Money
```c#
public int GetMoney(string player)
{

}
```
<bb><i><b>Descriptions: </b>Gets player's money amount.</i><dd>
<bb><i><b>Arguments: </b>player's name</i><dd>
<bb><i><b>Returns: </b>player's money amount</i><dd>

###### Set Money
```c#
public void SetMoney(Player player, int amount)
{

}
```
<bb><i><b>Descriptions: </b>Sets player's money amount.</i><dd>
<bb><i><b>Arguments: </b>player must be the Player (MiNET.Player), new money amount</i><dd>
