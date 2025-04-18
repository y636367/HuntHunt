<div align=center> 
  
<h1>HuntHunt 🔫</h1>
모바일로 플레이 할 수 있는 뱀서라이크<br>
플레이어를 해하기 위해 몬스터들이 달려든다. 이대로 당하고만 있을수는 없다! 무기를 들어라! <br>
무기를 해금하고 강화하여 달려드는 몬스터들을 무찌르자!😡<br>

<br>
<img src="https://github.com/user-attachments/assets/d96acd52-c894-4a47-9e4c-b3e650ec31f8">

</div>

## :calendar: 목차
  1. [개요](#page_with_curl-개요)
  2. [플레이 영상](#movie_camera-플레이-영상)
  3. [실행 방법](#memo-실행방법)
  4. [게임 설명](#video_game-게임설명)
  5. [게임 정보](#mag_right-게임정보)
  6. [트러블 슈팅](#loop-트러블슈팅)

## :page_with_curl: 개요
 - 프로젝트 이름: HuntHunt
 - 개발 기간: 2023.12-2024.03
 - 개발 목적 및 동기:<br><br>
플레이 하는 플랫폼에 맞춰 인게임 해상도 조절, ObjectPooling을 통한 몬스터, 아이템 과 같은 많은 오브젝트 관리 및<br>
OnParticle~ 과 같은 함수들의 기능 사용을 학습하기 위해 한때 예전에 재미있게 즐겼던 뱀서라이크 게임을 모바일에서도 간편하게 즐길 수 있도록 직접 제작해보았습니다.
 
 - 개발 엔진 및 사용언어: <img src="https://img.shields.io/badge/unity-000000?style=for-the-badge&logo=unity&logoColor=white"> / <img src="https://img.shields.io/badge/-C%23-512BD4?style=for-the-badge&logo=csharp&logoColor=white">
 - 사용 서버: 뒤끝(https://www.thebackend.io/)
 - :file_folder: [프로젝트 설명 PPT 다운로드](https://drive.google.com/uc?export=download&id=1RPtR8uUGa8GHim1wIAydF3OPEDmc-g-X)
   
## :movie_camera: 플레이 영상
[▶ 영상 보기](https://github.com/user-attachments/assets/67cfc27e-9864-48dc-8057-47c3bf2f7e92)

## :memo: 실행방법
  :warning:안드로이드 전용:warning:
 1. :file_folder: [게임 다운로드 링크](https://drive.google.com/file/d/1kNtFwgLYYHLHQ7dR6ghagj1f5NIm8DYE/view?usp=sharing)
 2. 위 링크를 클릭하여 'Hunt_Hunt.apk' 파일을 다운로드 후 설치 

 ## :video_game: 게임설명
<details>
<summary>게임 설명 보기</summary>
  
  - ### 로그인 및 회원가입(아이디, 비밀번호 찾기)

<div align=center> 
  
|<img src="https://github.com/user-attachments/assets/16816502-49dd-4231-967c-17f4f62455a6" width="200" height="400"/>|<img src="https://github.com/user-attachments/assets/02470d46-7f9c-4dad-b6a6-a784b9b234ae" width="200" height="400"/>|<img src="https://github.com/user-attachments/assets/ffc58924-2807-40ab-8060-b7f936b8ecd4" width="200" height="400"/>|<img src="https://github.com/user-attachments/assets/50c11f70-1585-4820-99fe-f8b2ee793ca3" width="200" height="400"/>|
|---|---|---|---|
|<div align=center>로그인 화면</div>|<div align=center>회원가입 화면</div>|<div align=center>아이디 찾기</div>|<div align=center>비밀번호 찾기</div>|

게임을 플레이 하기 위해선 계정을 생성하여야 하며 회원 가입 시 아이디, 비밀번호, 이메일 정도만 필요로 합니다.<br>
아이디 또는 비밀번호를 잊은 경우, 해당 탭을 통해 이메일 인증으로 쉽게 찾을 수 있습니다.<br>
(비밀번호의 경우 임시 비밀번호 발송, 인게임에서 수정요함)

</div>

  - ### 메인 화면(스테이지 선택)

<div align=center> 
  
|<img src="https://github.com/user-attachments/assets/13622e14-b25a-4013-a1ff-ee87da2ef82e" width="200" height="400"/>|<img src="https://github.com/user-attachments/assets/06de8e93-322f-4974-8296-116128a6f510" width="200" height="400"/>|<img src="https://github.com/user-attachments/assets/bed0f2f2-d4a6-43de-b80a-4fb059d46b56" width="200" height="400"/>|
|---|---|---|
|<div align=center>메인 화면</div>|<div align=center>스테이지 변경</div>|<div align=center>난이도 설정</div>|

스테이지 클리어 여부에 따라 플레이 할 스테이지를 대표이미지 옆 화살표 버튼을 조작해 선택 할 수 있습니다.<br>
'준비!' 버튼을 터치 하여 해당 스테이지의 플레이 할 단계를 선택 할 수 있으며<br>
난이도는 1단계부터 3단계 까지 일반 난이도와 끝없이 진행되는 무한 난이도로 구성되어 있습니다.<br>
난이도 별 소모되는 Life 는 다르기에 화면 상단의 중앙의 Life 소지량을 확인하시길 바랍니다.<br>

</div>

  - ### 메인 화면(무기 강화, 스테이지 선택, 플레이어 강화)

<div align=center> 
  
|<img src="https://github.com/user-attachments/assets/ac66ea69-25f8-49bf-86ce-24f3f2ede597" width="200" height="400"/>|<img src="https://github.com/user-attachments/assets/76be2655-b49f-4070-88b5-83fbb5d15116" width="200" height="400"/>|<img src="https://github.com/user-attachments/assets/e2dadee9-1d92-415f-95cb-25d0c67ee403" width="200" height="400"/>|<img src="https://github.com/user-attachments/assets/e01ace62-0274-4df0-9fe0-7412ac16fb3e" width="200" height="400"/>|
|---|---|---|---|
|<div align=center>무기 목록 화면</div>|<div align=center>무기 강화 화면</div>|<div align=center>스테이터스 목록 화면</div>|<div align=center>스테이터스 강화 화면</div>|

게임을 좀 더 쾌적하게 플레이 하기 위해 다양한 무기를 선택 하여 플레이 할 수 있으며 원하는 무기를 강화하며 사용 할 수 있습니다.<br>
플레이어의 스테이터스를 강화 하여 최대 체력, 공격력, 이동 속도 등을 강화 할 수 있습니다.<br>
화면 상단 우측의 소지 코인을 확인하여 어떠한 것을 강화할 지 잘 선택 하시길 바랍니다.<br>

</div>

</details>

## :mag_right: 게임정보
<details>
<summary>게임 정보 보기</summary>
  
 - ### 인게임 화면
스테이지

<div align=center>

|<img src="https://github.com/user-attachments/assets/6803cd04-998c-419b-845b-5bbfc1c7b892" width="200" height="400"/>|<img src="https://github.com/user-attachments/assets/b011a9da-8b8f-4250-917a-b686cd9de788" width="200" height="400"/>|<img src="https://github.com/user-attachments/assets/747122db-c6c1-4fc6-b561-78cb898dc2cd" width="200" height="400"/>|<img src="https://github.com/user-attachments/assets/f4f3bb1e-1e31-45c0-91d9-8694eab4cc1f" width="200" height="400"/>|<img src="https://github.com/user-attachments/assets/728b1c7a-0ea3-4a89-a373-72fed64a92a7" width="200" height="400"/>|
|---|---|---|---|---|
|<div align=center>1 스테이지 - 🌲</div>|<div align=center>2 스테이지 - 🌑</div>|<div align=center>3 스테이지 - ⛺</div>|<div align=center>4 스테이지 - ✨</div>|<div align=center>5 스테이지 - 🛤️</div>|

1, 2, 3 스테이지는 한정된 공간에서, 4, 5 스테이지는 루프되는 공간에서 게임을 진행할 수 있습니다.<br>
화면을 터치하여 캐릭터를 직접 조종하자!

<br>
기타

|<img src="https://github.com/user-attachments/assets/1f5be579-af38-4ed2-9f8b-7d701e4a32ba" width="200" height="400"/>|<img src="https://github.com/user-attachments/assets/6ac3004e-4212-4465-b590-efbf49df2ea8" width="200" height="400"/>|<img src="https://github.com/user-attachments/assets/d477a04f-79f5-47ce-abe8-5e58c96396ca" width="200" height="400"/>|<img src="https://github.com/user-attachments/assets/8dcf0e5a-9514-4d03-9d34-faaeb20569a1" width="200" height="400"/>|
|---|---|---|---|
|<div align=center>인게임 레벨</div>|<div align=center>보물상자 획득</div>|<div align=center>현재 진행 상황 확인</div>|<div align=center>게임 오버 시 스코어</div>|

인게임 내 몬스터 처치 후 드랍된 경험치 조각 일정량 획득 시 레벨업!<br>게임 진행을 도와주는 특성 획득!<br>
(오직 인게임 내부에서만 획득할 수 있는 무기가 있으니 직접 게임을 플레이해서 확인해보자!)<br>
보스를 처치 하면 드랍되는 보물 상자를 획득 하면 랜덤하게 특성을 획득 할 수 있습니다. 적절히 활용하여 게임을 클리어 해봅시다!<br>
중간중간 내가 어떠한 특성을 획득했는지, 현재 스텟은 어떠한지 일시정지 하여 확인할 수 있습니다.<br>(무기, 특성 터치 시 세부 확인 가능!)<br>
게임 종료 시 직전까지의 킬 스코어, 획득한 코인, 생존한 시간등을 종합하여 플레이어의 경험치로 환산, <br>
플레이어의 레벨이 상승하면 최대 소지할 수 있는 Life가 증가합니다!

<br>
몬스터

|<img src="https://github.com/user-attachments/assets/c0a9910d-718f-4bf2-b336-c89087958a35" width="200" height="400"/>|
|---|
|<div align=center>보스 등장</div>|

3분 마다 중간 보스가 등장! 잘 회피하면서 보스를 사냥해 살아남자!<br>
15분 마다 히든 보스가 등장! 중간 보스와 달리 스킬을 사용하니 조심하자!<br>

<br>
히든 보스

|<img src="https://github.com/user-attachments/assets/6bf03e03-8f9a-4d2d-9546-c13866ea1a4d" width="250" height="400"/>|<img src="https://github.com/user-attachments/assets/fbe27d33-07cc-43b0-b7e8-22508b3c9d54" width="250" height="400"/>|<img src="https://github.com/user-attachments/assets/35bb14fc-2aa0-4501-aab6-0e473afae74d" width="250" height="400"/>|<img src="https://github.com/user-attachments/assets/39bb6e90-c98f-4f62-af4f-6031eb2bc4f0" width="250" height="400"/>|<img src="https://github.com/user-attachments/assets/f97db332-2bd6-445d-a58c-cd9f75e12d04" width="250" height="400"/>|<img src="https://github.com/user-attachments/assets/a604789f-91e4-4e28-8c5e-40bb623560d3" width="250" height="400"/>|
|---|---|---|---|---|---|
|<div align=center>고스트</div>|<div align=center>풍뎅이</div>|<div align=center>스컬 킹</div>|<div align=center>머쉬 킹</div>|<div align=center>등푸른 거북이</div>|<div align=center>레드 슬라임</div>|

</div>

<br>
 - 고스트 : 필드에서 사라져 일정시간 이 지나면 플레이어 근처에서 랜덤하게 다시 등장한다.<br>
 - 풍뎅이 : 플레이어를 향해서 돌진한다.<br>
 - 스컬 킹 : 자신의 체력을 일부 회복한다.<br>
 - 머쉬 킹 : 아무런 아이템을 드랍하지 않는 일반 몬스터를 소환합니다.<br>
 - 등푸른 거북이 : 회전하며 필드를 이리저리 충돌하며 플레이어를 위협합니다.<br>
 - 레드 슬라임 : 자신을 본딴 분신을 소환합니다.<br>

</details>

## :loop: 트러블슈팅
<details>
<summary>트러블슈팅 보기 (해상도 및 시야각 조절, 벽 뚫림)</summary>
  
  - ### 모바일 기기별 해상도 대응 및 3D 화면 카메라 시야각 문제
<br>
<div align=center> 
  
|<img src="https://github.com/user-attachments/assets/33dc5d73-812b-40d0-b13e-c99f05257f86" width="400" height="240"/>|<img src="https://github.com/user-attachments/assets/6ba74530-2b11-4c33-b75a-04349be2e1ea" width="400" height="240"/>|
|---|---|

</div>
<br>

 - 문제 : 플레이 하는 플랫폼별 화면의 해상도가 맞지 않는 문제 발생
 - 원인 : Canvas Scaler의 UI스케일 모드 미 적용, 3D 인게임 화면에서 종횡비 시야각 설정 코드 미 존재
 - 해결 : Canvas Scaler의 스케일 모드를 '화면 크기에 따라' 로 설정 후 비율 설정<br>
 3D 인게임 시야각 문제는 현재 플랫폼의 종횡비를 계산하여 동적으로 FOV(Field of View)를 재설정

<br>

  - ### 스테이지 벽이 뚫리는 문제
<br>
<div align=center> 
  
|<img src="https://github.com/user-attachments/assets/ac8fcc61-0e2a-440e-87ed-ba506f7c1087" width="400" height="240"/>|<img src="https://github.com/user-attachments/assets/6949aaa7-8746-436a-879f-214b3bd84a77" width="400" height="240"/>|
|---|---|

</div>
<br>

 - 문제 : 캐릭터가 물리적 충돌로 인해 필드 밖으로 벗어나는 문제가 발생
 - 원인 : 캐릭터 정면 Raycast 미 사용으로 인한 조작 불가 범위 미 설정
 - 해결 : 캐릭터의 정면에 Raycast를 통해 일정 거리 앞 경계면 도달 시 해당 방향으로의 조작 불가로 설정

</details>
