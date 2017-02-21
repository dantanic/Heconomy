<img src="http://i.imgur.com/cU6s6a9.jpg"/>

# HerbPlugins 저장소에 오신것을 환영합니다.

이 단체는 제가 만든 플러그인의 저장소이며, 당신은 다운로드 할 수 있습니다.

# 개요

### Q. 이것은 무엇인가요?

진보적인 MiNET 경제 플러그인 입니다.
<br>
<br>
Heconomy 플러그인을 사용함으로써 당신의 서버의 경제를 관리하실 수 있습니다.
<br>
Heconomy 플러그인과 함께하면 당신의 돈을 보거나, 다른 사람에게 돈을 지불하는...등

### Q. 저는 무엇을 준비해야 하나요?

MiNET 1.0.1322 버젼 구동기랑 NET Framework 4.6.1 버젼을 설치해야 합니다.

### Q. 명령어를 어떻게 사용하나요?

> | Command | Description | Permission | Arguments |
> | :-------: | :-------: | :-------: | :-------: |
> | `/money` | 당신의 돈 수량을 보여줍니다. | 기본 |  |
> | `/pay` | 사람에게 돈을 지불합니다. | 기본 | `<player: 문자열> <amount: 자연수>` |  |

### Q. 퍼미션이 있나요?

<dd><i><b>heconomyapi.*</b> - HeconomyAPI 퍼미션 입니다..</i></dd>
<dd><i><b>heconomyapi.command.money</b> - HeconomyAPI 명령어 Money 퍼미션입니다.</i></dd>
<dd><i><b>heconomyapi.command.pay</b> - HeconomyAPI 명령어 Pay 퍼미션입니다.</i></dd>

### Q. 사람의 돈 데이터를 어떻게 관리하죠?

<dd><i><b>예시.json (예시: 사람의 이름)</b></i><dd>

```json
{
   "Money" : 10
}
```

당신은 **Money** 구문을 통해 사람의 돈 데이터를 관리할 수 있습니다.

### Q. 스크린샷을 볼 수 있을까요?

<img src="http://i.imgur.com/mMcVJJQ.jpg"/>
<br>
<img src="http://i.imgur.com/4mcjFmc.jpg"/>

### Q. API 메소드를 지원하나요?

우리는 개발자들을 위한 HeconomyAPI 메소드를 지원하고 있으며, 이 절차를 읽으신후 시도해보시길 바랍니다.
<br>
<br>
<bb><i><b>HeconomyAPI api 를 등록하는 방법.</b></i><dd>

*I. HeconomyAPI 플러그인을 다운로드 합니다.*
<br>
<br>
*II. HeconomyAPI api 를 당신의 C# 구문에 추가합니다.*

```c#
using HeconomyAPI;
```

*III. API 메소드를 불러옵니다.*

```c#
HeconomyAPI.GetAPI().function;
```

*예시 소스.*

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

<bb><i><b>API 메소드.</b></i><dd>

###### 돈의 단위
```c#
public string GetMoneySymbol()
{

}
```
<bb><i><b>설명: </b>돈의 단위값을 가져옵니다.</i><dd>
<bb><i><b>반환: </b>돈의 단위</i><dd>

###### 돈의 기본수량
```c#
public int GetDefaultMoney()
{

}
```
<bb><i><b>설명: </b>돈의 기본적인 수량을 가져옵니다.</i><dd>
<bb><i><b>반환: </b>기본적인 돈 수량</i><dd>

###### 최소한의 돈 수량
```c#
public int GetMinimumMoney()
{

}
```
<bb><i><b>설명: </b>최소한의 돈 수량을 가져옵니다.</i><dd>
<bb><i><b>반환: </b>최소한의 돈 수량</i><dd>

###### 돈
```c#
public int GetMoney(string player)
{

}
```
<bb><i><b>설명: </b>사람의 돈 수량을 가져옵니다.</i><dd>
<bb><i><b>Arguments: </b>사람의 이름</i><dd>
<bb><i><b>반환: </b>사람의 돈 수량</i><dd>

###### 돈 설정
```c#
public void SetMoney(Player player, int amount)
{

}
```
<bb><i><b>설명: </b>사람의 돈 수량을 설정합니다.</i><dd>
<bb><i><b>Arguments: </b>사람은 Player 의 인자여야 합니다 (MiNET.Player), 새로운 돈 수량</i><dd>
