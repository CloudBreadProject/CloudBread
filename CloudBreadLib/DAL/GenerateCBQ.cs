using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

using Newtonsoft.Json;  // 추가
using Newtonsoft.Json.Schema;  // 추가

namespace CloudBreadLib.DAL.GenerateCBQ
{
    public class GenerateCBQ
    {

        //// 예제 처리
        //S = SET 쿼리 구문 SET NOCOUNT ON / SET XACT_ABORT ON
        //BT = BEGIN TRAN
        // Q = QUERY 계층 루트
        //  QT = 쿼리타입 식별자 I / U / D / S
        //  T = TABLE 테이블
        //  C = COLUMNS 컬럼리스트
        //  V = VALUES 입력 값들
        //  W = WHERE 조건
        //  O = ORDER BY 정렬
        //  J = JOIN 구문
        //  L = SELECT의 LOCK 옵션
        //CT = COMMIT TRAN

        //테스트 JSON 문자열
        //{
        //    "s": "set nocount on",
        //    "bt": "begin tran",
        //    "q": [
        //    {
        //        "qt": "i",
        //        "t": "member",
        //        "c": "member.memberid, member.membername",
        //        "v": "1, '구름빵'",
        //        "w": "",
        //        "o": "",
        //        "j": "",
        //        "nl": ""
        //    },
        //    {
        //        "qt": "u",
        //        "t": "member",
        //        "c": "member.memberid, member.membername",
        //        "v": "2, '프로젝트'",
        //        "w": "member.memberid = 1",
        //        "o": "",
        //        "j": "",
        //        "nl": ""
        //    },
        //    {
        //        "qt": "d",
        //        "t": "member",
        //        "c": "",
        //        "v": "",
        //        "w": "member.memberid=1 and member.membername = '프로젝트'",
        //        "o": "",
        //        "j": "",
        //        "nl": ""
        //    },
        //    {
        //        "qt": "s",
        //        "t": "member",
        //        "c": "member.memberid,      member.membername",
        //        "v": "",
        //        "w": "member.memberid=1,       member.membername like '회원'",
        //        "o": "orderby",
        //        "j": "",
        //        "nl": ""
        //    },
    
        //    ],
        //    "ct": ""
        //}


        // 스트링 빌더 append 준비
        StringBuilder sbQuery = new StringBuilder();

        // JSON 스트링을 받음
        public static string Json = @"{'s':'set nocount on','bt':'begin tran','q':[{'qt':'i','t':'member','c':'member.memberid, member.membername','v':'1, \'구름빵\'','w':'','o':'','j':'','nl':''},{'qt':'u','t':'member','c':'member.memberid, member.membername','v':'2, \'프로젝트\'','w':'member.memberid = 1','o':'','j':'','nl':''},{'qt':'d','t':'member','c':'','v':'','w':'member.memberid=1 and member.membername = \'프로젝트\'','o':'','j':'','nl':''},{'qt':'s','t':'member','c':'member.memberid,      member.membername','v':'','w':'member.memberid=1,       member.membername like \'회원\'','o':'orderby','j':'','nl':''},],'ct':''}";

        // 스키마 파일로 검사
        // string schemaJson = @"{'description':'CB Query','type':'object','properties':{'s':{'type':'string'},'bt':{'type':'string'},'qt':{'type':'string'},'t':{'type':'string'},'c':{'type':'string'},'v':{'type':'string'},'w':{'type':'string'},'j':{'type':'string'},'i':{'type':'string'},'l':{'type':'string'},'ct':{'type':'string'}}}";


        //JsonSchema schema = JsonSchema.Parse(schemaJson);


        //JSON Validating 실행   http://www.newtonsoft.com/json/help/html/JsonSchema.htm
 
        //JSON으로 파싱 스키마 validation 수행


        //루프를 돌면서  스트링빌더에 append 수행

        // public static string json = @"['Starcraft','Halo','Legend of Zelda']";
        // List<string> videogames = JsonConvert.DeserializeObject<List<string>>(json);

        List<string> query = JsonConvert.DeserializeObject<List<string>>(Json);
        


        // INSERT일 경우

        //UPDATE일 경우

        // DELETE일 경우

        // SELECT 일 경우

        // select는 마지막 한번만 존재 해야함
        //commit tran 위치 조심(마지막으로 select 앞에)



    }
}
