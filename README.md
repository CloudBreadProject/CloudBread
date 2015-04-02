# CloudBread
클라우드 기반 무료 오픈소스 프로젝트로, 모바일 게임과 모바일 앱에 최적화된 게임 서버 엔진입니다.
모든 서비스는 마이크로소프트의 클라우드 서비스인 Azure에 최적화되어 동작하며, 안정성과 확장성을 목표로 개발 중입니다.

<h2>기능</h2>
<h3>PaaS / DaaS 서버 엔진</h3>
- PaaS, DaaS 로 서비스 즉시 배포
- Real Auto Scale - PaaS
- 개발/테스트/배포 = 통합 환경
- 서비스 규모에 따른 앱 변경 없음

<h3>글로벌 론칭 아키텍처</h3>
- 글로벌 론칭+데이터 동기화
- 설계 부터 클라우드에 최적화된 아키텍처 및 프레임워크로 개발
- 오픈소스 프레임워크 활용 개발

<h3>보안, 관리, 기술교육</h3>
- 저장/통신에 표준 암호화 기술 적용
- 기본 관리자 서비스 및 커스터마이징
- 분석/관리 배치 작업 추가 제작 가능 
- 무료 기술 교육 제공(헤카톤/캠프)


<h2>개발자 그룹</h2>
페이스북 사용자 그룹 : https://www.facebook.com/groups/cloudBreadProject/ 

<h2>지원되는 환경</h2>

<h2>설치</h2>
- Wiki의 튜토리얼 설치 참조

<h2>프로젝트 설명</h2>
모바일게임과 모바일 앱에서 사용되는 사용자의 패턴과 액션을 기록해 기능들을 제공
클라이언트 모바일 디바이스는 게임서버로 JSON 방식의 데이터를 요청하고 서버가 해당 데이터를 처리 후 응답
약 100여개의 비즈니스 로직이 기본제공되며(Wiki 참조) 클라이언트는 마이크로소프트가 오픈소스로 직접 만들어 제공하는 라이브러리를 통해 서버로 API를 호출

<h3>Example 1. 로그인ID 중복 체크 </h3>
Behavior : SelLoginIDDupeCheck
- 최초 멤버등록 절차에서 멤버ID 정보를 서버로 전달해 중복ID 인지 여부 응답

요청 데이터
{
  memberID = “member1”
}

응답 데이터 
0 또는 1

- 이런 식으로, JSON 데이터를 요청하고 응답이 클라이언트로 전달됨.
- 클라이언트 개발자는 파라미터에 맞춰 JSON 데이터를 호출하고 응답을 받아 게임 서버에 저장

<h3>Example 2. 로그인 후 게임 이벤트 조회 </h3>
Behavior : SelGameEvents
- 멤버가 참여 가능한 이벤트가 존재하는지 여부 체크

요청 데이터
{
  memberID = “member1”
}

응답 데이터 
{
  EventID =“eventID1”, EventName=“크리스탈1”, 기간=…
  EventID =“eventID2”, EventName=“좋은템1”, 기간=…
  EventID =“eventID13, EventName=“보너스아이템”, 기간=…
}

- 요청 데이터와 응답 데이터는 각 API마다 목적과 사용 방식이 존재. Wiki의 API 설명 참조

<h3>Example 3. 멤버등록-회원가입 </h3>
Behavior : InsRegMember
- 멤버등록 처리

요청 데이터
{
  	memberID = “member1”, email =“abc@email.com”, name=“코난”, …
}

응답 데이터 
2 – (영향을 받은 row의 숫자)


내 앱에서 필요한 비즈니스 로직과 API 리스트에 따라 원하는 시나리오로 API를 호출하면 게임 서버 처리 완료. 
자신이 필요한 API만 호출하면 됨. Wiki 참조
- 앱내 구매만 사용할 경우
- 회원+포인트 리더보드만 필요한 경우

<h2>질문/토론</h2>
페이스북 사용자 그룹 : https://www.facebook.com/groups/cloudBreadProject/ 

