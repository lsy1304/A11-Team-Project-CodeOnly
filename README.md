<div align="center">
  
![던전 입장](https://github.com/lsy1304/A11-Team-Project/assets/127918879/4419da38-da70-456a-9f41-7687db1645c5)



</div>

# 📖목차

1. [프로젝트 소개](#프로젝트-소개)
2. [팀소개](#팀소개)
3. [프로젝트 계기](#프로젝트-계기)
4. [주요기능](#주요기능)
5. [개발기간](#개발기간)
6. [기술스택](#기술스택)
7. [와이어프레임](#와이어프레임)
8. [Trouble Shooting](#trouble-shooting)
9. [자체평가](#자체평가)
10. [시연영상](#시연영상)
    
<br>
<br>
<br>
<div align="center">
  
# 프로젝트 소개

### 게임 이름

#### **`Re:SpartaDungeon`**

<br>

### 게임 컨셉

####   **`턴제 Rpg 리뉴얼`**
#### **레퍼런스 - 스타레일**
   
<br>
<br>
<br>

# 팀소개
### 내일배움캠프 Unity 4기 심화 프로젝트 A 11조 Pick Me UP!

<br>

### 👨‍👨‍👦 **멤버구성**

팀장 : **이승영**<br>
팀원 : **이종민**<br>
팀원 : **최재원**<br>
팀원 : **김영선**

<br>
<br>
<br>

# 프로젝트 계기
갑작스러운 자유 주제에 방황하다가 지금까지 배운 내용을 참고하여 이전에 코드로만 만들었던 Sparta Text RPG를 리뉴얼해서 만들어보자! 라는 생각이 들어 프로젝트를 기획하게 되었습니다.<br>

<br>
<br>
<br>

# 주요기능

### 조작
![조작법 설명](https://github.com/lsy1304/A11-Team-Project/assets/127918879/3c5aa2e3-37b2-4dcc-b4a6-bd880f2bf5e8)

### 상점
![image](https://github.com/lsy1304/A11-Team-Project/assets/127918879/382f1b31-63eb-4238-a6ea-fb32ecfc1590)
![image](https://github.com/lsy1304/A11-Team-Project/assets/127918879/86f1ebe2-002b-461c-84ba-11c806b3c9d8)
![image](https://github.com/lsy1304/A11-Team-Project/assets/127918879/7e2cd0ab-8c05-47d3-9904-93ae8b15a929)

### 상점 아이템 구매
![image](https://github.com/lsy1304/A11-Team-Project/assets/127918879/cb5579d2-bf0e-4e2c-93f3-8ba88fe78591)

### 인벤토리
![image](https://github.com/lsy1304/A11-Team-Project/assets/127918879/7580a04e-a4bd-4b3c-b402-c61a45c3decf)

### 아이템 장착
![image](https://github.com/lsy1304/A11-Team-Project/assets/127918879/0c390b93-ba7d-4a2f-888a-b62626663254)

### 맵 자동 생성
![맵 자동 생성](https://github.com/lsy1304/A11-Team-Project/assets/127918879/308b0f74-0f1e-438b-bb7e-9164ec72ce59)

![image](https://github.com/lsy1304/A11-Team-Project/assets/127918879/55478ae3-1666-4559-8a79-597482b237b3)

### 전투
#### 플레이어 선공
![전투 - 플레이어 선공](https://github.com/lsy1304/A11-Team-Project/assets/127918879/dbaf535d-9900-4a8d-913d-83753fefa6c8)

<br><br>

#### 적 선공
![전투 - 적 선공](https://github.com/lsy1304/A11-Team-Project/assets/127918879/40f79fe1-3073-4852-9757-aab8f9c51753)

<br>
<br>
<br>

# 개발기간
24.06.19 ~ 24.06.26

<br>
<br>
<br>

# 기술스택
### 언어
**C#**

### 프레임워크 및 라이브러리
**Unity**: 게임 개발 프레임워크<br>

### 개발 도구
**Visual Studio**: 통합 개발 환경(IDE)<br>
**GitHub**: 버전 관리 및 소스 코드 저장소

<br><br><br>
### 기술 스택과의 관계
**Unity**: 클라이언트 개발에 사용되었습니다.<br>
**C#**: 게임 로직과 데이터 처리에 사용되었습니다.<br>
**Visual Studio**: 전체 개발 환경으로 사용되었습니다.<br>
**GitHub**: 코드 버전 관리 및 협업을 위한 플랫폼으로 사용되었습니다.

<br><br><br>

# 와이어프레임
![image](https://github.com/lsy1304/A11-Team-Project/assets/127918879/596950de-2bc9-48b5-8b13-63b15b4d5bb2)


<br><br><br>

<div align="center">
  
# Trouble Shooting
</div>

### 배틀 씬 구현
레퍼런스로 삼았던 스타레일과 같이 적을 공격하여 전투에 진입하고, 캐릭터를 조종하여 이동하는 씬과는 다른 배틀 씬에서 전투가 이루어지도록 하고 싶어서 어떻게 배틀 씬을 구현해야하나 고민<br>
전투가 끝나고 이전 씬으로 돌아와야하고, 배틀 씬에서 주변 지형이 보였으면 좋겠다고 생각하여 LoadSceneMode.Additive로 배틀 씬을 구현하기로 결정. <br>

#### 1차 원인 :<br>
배틀에 진입할 경우 충돌이 발생한 플레이어와 적을 소환 한다는 개념으로 구현하려고 했으나 <br>
Additive모드로 씬을 로드한 후 Instatiate로 오브젝트를 생성하면 새롭게 로드된 씬에서 생성되는 것이 아니라 기존 씬에서 생성이 되는 문제가 발생.<br>

#### 1차 해결 :<br>
setActiveScene을 사용해 새롭게 불러온 씬을 activScene으로 설정하고 오브젝트를 인스턴스화 한 후, <br>
해당씬으로 이동시키는 작업을 추가하여 해결<br><br>

![carbon (1)](https://github.com/lsy1304/A11-Team-Project/assets/127918879/56f5730d-a2aa-4b23-b5a4-9b746731d656)

#### 2차 원인 :<br>
매번 적과 플레이어를 생성하고 옮기고 하는 작업이 불필요하다고 느껴서 팀원과 회의.<br>

#### 2차 해결 :<br>
회의 끝에 데이터만 입히자는 팀원들의 의견을 따라 배틀 씬에는 미리 플레이어와 적의 고정된 위치를 세팅해두고, <br>
플레이어의 스탯, hp 상황, 위치 정보를 불러와 미리 세팅되어 있는 플레이어 오브젝트에 데이터를 덮어씌우고, 적은 플레이어에게서 일정거리 띄워진 곳으로 옮긴 후 충돌한 적의 데이터를 불러와 입히는 것으로 구현.<br><br>

![carbon (2)](https://github.com/lsy1304/A11-Team-Project/assets/127918879/da1cf150-148f-4169-a9fa-96629a67f1bd)

<br><br>

### 공격 -> 이동 애니메이션 전환이 상태 전환에 비해 느린 문제

원인 : 공격 -> Exit 트랜지션에서 Transition Duration 값이 존재하여 애니메이션 전환에서 딜레이가 발생<br>
해결법 : 공격 -> Exit 트랜지션에서 Transition Duration 값을 0으로 설정<br><br>

![image](https://github.com/lsy1304/A11-Team-Project/assets/127918879/cb2dfc40-9d06-4049-8d21-ca4d765809f4)

<br><br>

### RayCast와 TryGetComponunt로 상점을 검출 할 때 가끔씩 NullExceptionError 발생

원인 : RayCast에서 검출이 안되면 hit 자체가 null이므로 hit.collider에서 NullExceptionError 발생<br>
해결법 : RayCast에서 상점이 검출이 되지 않는 경우 return으로 강제로 반환<br><br>

![image](https://github.com/lsy1304/A11-Team-Project/assets/127918879/7d26a185-5cd2-480c-8dff-a4c204a8838d)


### NavMesh를 이용하여 동적으로 생성되는 맵에 Bake를 사용하니 적 오브젝트가 제자리 걸음 문제 발생

원인 : 오브젝트 하나하나가 Bake가 되어 NavMesh층이 여러 개가 쌓이게 되므로, 오브젝트가 움직이지 않게되었다<br>

해결법 : 바닥으로 사용할 Floor 오브젝트에 NavMeshSurface 기능을 이용하여 오브젝트가 생성된 이후에 Bake를 할 수 있도록 변경<br><br>
![image](https://github.com/lsy1304/A11-Team-Project/assets/127918879/b6ab6337-f8fe-4d69-bbfe-2f223f2b0011)


<br><br>

# 자체평가

### 김영선

UML의 작성과 초기 기획을 탄탄히 하는 것이 중요하다는 것을 다시금 뼈져리게 느꼈다.<br>
다른 사람이 구현하는 내용에 따라서 달라지는 부분 이었던 만큼 초반에 어느정도 함께 정하고 시작했더라면 기능분리도 잘되고 코드를 더 가독성 있게 잘 짤 수 있었을 것 같다

<br><br>

### 이승영

초반 기획을 중요성을 뼈저리게 느낀 것 같다. 다음 프로젝트에서는 2~3일이 걸리더라도 초반 기획을 탄탄히 잡고 가야 할 것 같다.

<br><br>
### 이종민

개발 기간을 자세히 설정하지 않아 항상 시간에 쫓기며 개발하여 게임 자체의 부족함이 많았다. 처음 시도해 보는 분야가 많아 제작 시간이 오래 걸렸다. 
제작할 기능에 대해 자세히 알아보고 기능을 구현하는 것도 중요하다고 생각되었다. 팀원과의 의사소통을 조금 더 활발히 했다면, 코드 가독성을 높이고 
불필요한 코드를 줄일 수 있었다.


<br><br>
### 최재원

초반 기획이 적어 프로젝트 방향성이 흐릿했던거 같다. 이번 프로젝트에 몸이 아파 제대로 참여를 잘 못해서 맡은 부분 기능이 아쉽고 팀원분들한테 많이 미안했습니다.
<br><br>
<div align="center">
  
# 시연영상
https://youtu.be/k8AVcASWlTo
<br><br><br>
