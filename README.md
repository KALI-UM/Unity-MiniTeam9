# 🛡️ 구해줘!! 마족마을 (운빨존많겜 모작)


🛠️ **개발 도구**
<img src="https://img.shields.io/badge/C%23-80247B?style=flat-square&logo=csharp&logoColor=white"/> <img src="https://img.shields.io/badge/Unity-000000?style=flat-square&logo=unity&logoColor=white"/> <img src="https://img.shields.io/badge/Dotween-005E9D?style=flat-square&logo=dotween&logoColor=white"/> <img src="https://img.shields.io/badge/EasyTutorial-005E9D?style=flat-square&logo=easytutorial&logoColor=white"/>

📅 **개발 기간**
25.01-21 ~ 25.02.14 (4주)

랜덤 타워 디팬스 장르의 **운빨존많겜**을 모작한 프로젝트입니다.

**그만쫌쳐들어와**의 리소스를 활용했습니다.

---

## 🛠️ 주요 구현 요소
<table>
  <tr>
    <td align="center"><strong>타워 선택, 합체, 판매</strong></td>
    <td align="center"><strong>타워 이동</strong></td>
    <td align="center"><strong>최상위 타워 진화</strong></td>
  </tr>
  <tr>
    <td><img src="./Screenshot/TowerInfo.jpg" width="250"/></td>
    <td><img src="./Screenshot/TowerMove.jpg" width="250"/></td>
    <td><img src="./Screenshot/TowerFusion.jpg" width="250"/></td>
  </tr>
</table>

- **타워 상호작용**
  - 타워를 선택해서 타워 판매, 합체, 이동
  - 골드를 지불하여 랜덤 타워 소환
  - 타워를 재료로 최상위 타워로 진화 👉 [RecipeProgressTracker.cs](https://github.com/KALI-UM/Unity-MiniTeam9/blob/main/Assets/Scripts/RecipeProgressTracker.cs#L70), [MaxFusionSystem.cs](https://github.com/KALI-UM/Unity-MiniTeam9/blob/main/Assets/Scripts/MaxFusionSystem.cs#L45)
  - 공격 범위 내의 적 공격에서 Cell단위 계산으로 거리 검사량을 줄임 👉 [TowerAttack.cs](https://github.com/KALI-UM/Unity-MiniTeam9/blob/main/Assets/Scripts/TowerAttack.cs#L153)
- **적**
  - 데이터테이블 데이터를 기반으로 적을 소환하는 웨이브 진행
  - 오브젝트 풀 적용

- 게임 오브젝트를 캡처해 png파일로 생성하는 **아이콘 이미지 캡처** 에디터 툴 개발 👉 [Capture Studio](https://github.com/KALI-UM/CaptureStudio)
- **타워, 적 프리펩 일괄 제작** 에디터 툴 개발 👉 [TowerPrefabEditor.cs](https://github.com/KALI-UM/Unity-MiniTeam9/blob/main/Assets/Editor/TowerPrefabEditor.cs)
  
