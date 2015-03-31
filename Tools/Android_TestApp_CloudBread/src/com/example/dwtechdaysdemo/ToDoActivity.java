package com.example.dwtechdaysdemo;

import static com.microsoft.windowsazure.mobileservices.MobileServiceQueryOperations.*;

import java.net.MalformedURLException;
import java.util.List;
import java.util.UUID;

import android.app.Activity;
import android.app.AlertDialog;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.TextView;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.microsoft.windowsazure.mobileservices.ApiJsonOperationCallback;
import com.microsoft.windowsazure.mobileservices.ApiOperationCallback;
import com.microsoft.windowsazure.mobileservices.MobileServiceClient;
import com.microsoft.windowsazure.mobileservices.MobileServiceTable;
import com.microsoft.windowsazure.mobileservices.NextServiceFilterCallback;
import com.microsoft.windowsazure.mobileservices.ServiceFilter;
import com.microsoft.windowsazure.mobileservices.ServiceFilterRequest;
import com.microsoft.windowsazure.mobileservices.ServiceFilterResponse;
import com.microsoft.windowsazure.mobileservices.ServiceFilterResponseCallback;
import com.microsoft.windowsazure.mobileservices.TableOperationCallback;
import com.microsoft.windowsazure.mobileservices.TableQueryCallback;


public class ToDoActivity extends Activity {

	/**
	 * Mobile Service Client reference
	 */
	private MobileServiceClient mClient;

	private TextView mText_request;
	private TextView mText_result;
	private EditText mEdit_service_name;
	private EditText mEdit_service_key;
	private CheckBox mchk_encrytion;
	/**
	 * Progress spinner to use for table operations
	 */
	private ProgressBar mProgressBar;

	/**
	 * Initializes the activity
	 */
	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_to_do);
		

		mText_request = (TextView) findViewById(R.id.text_request);
		mText_result = (TextView) findViewById(R.id.text_result);
		
		mEdit_service_name = (EditText) findViewById(R.id.edit_service_name);
		mEdit_service_key = (EditText) findViewById(R.id.edit_service_key); 
		mchk_encrytion = (CheckBox) findViewById(R.id.chkEncryption);
		
		try {
			// Create the Mobile Service Client instance, using the provided
			// Mobile Service URL and key
			mClient = new MobileServiceClient(
					mEdit_service_name.getText().toString(),
					mEdit_service_key.getText().toString(),
					this).withFilter(new ProgressFilter());		

		} catch (MalformedURLException e) {
			mText_result.setText("There was an error creating the Mobile Service. Verify the URL");
		}
	}

	
	public void SelLoginIDDupeCheck(View view) {
		if (mClient == null) {
			return;
		}
		JsonObject request = new JsonObject();
		String key = "1234567890123456";
		if(mchk_encrytion.isChecked())
		{
			try {
				request.addProperty("memberID", AES256Cipher.AES_Encode("aaa", key));
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		else
			request.addProperty("memberID", "aaa");
		mText_request.setText(request.toString());;
		mText_result.setText("");
		mClient.invokeApi("CBSelLoginIDDupeCheck", request, new ApiJsonOperationCallback() {
		    
		    @Override
		    public void onCompleted(JsonElement result, Exception error,
		            ServiceFilterResponse response) {
		    	    if (error == null) {
		    	    	mText_result.setText(result.toString());
			        } else {
			        	mText_result.setText(error.getMessage());
			        }
		    }
		});
	}
	
	public void InsRegMember(View view) {
		if (mClient == null) {
			return;
		}
		String key = "1234567890123456";
		JsonObject request = new JsonObject();
		UUID uuid = java.util.UUID.randomUUID();

		if(mchk_encrytion.isChecked())
		{
			try {
				request.addProperty("memberID", AES256Cipher.AES_Encode("회원ID-" + uuid, key));
				request.addProperty("memberPWD", AES256Cipher.AES_Encode(SHA_encrypt.encryption(uuid.toString()), key)); //password는 SHA-512기본
				request.addProperty("emailAddress", AES256Cipher.AES_Encode("GUID@GUID.com", key));
				request.addProperty("emailConfirmedYN", AES256Cipher.AES_Encode("N", key));			
				request.addProperty("phoneNumber1", AES256Cipher.AES_Encode("전화번호1-" + uuid, key));
				request.addProperty("phoneNumber2", AES256Cipher.AES_Encode("전화번호2-" + uuid, key));
				request.addProperty("piNumber", AES256Cipher.AES_Encode("개인식별번호-" + uuid, key));
				request.addProperty("name1", AES256Cipher.AES_Encode("이름1-" + uuid, key));
				request.addProperty("name2", AES256Cipher.AES_Encode("이름2-" + uuid, key));
				request.addProperty("name3", AES256Cipher.AES_Encode("이름3-" + uuid, key));
				request.addProperty("dob", AES256Cipher.AES_Encode("19990101", key));
				request.addProperty("recommenderID", AES256Cipher.AES_Encode("추천인ID-" + uuid, key));
				request.addProperty("memberGroup", AES256Cipher.AES_Encode("회원그룹-" + uuid, key));
				request.addProperty("lastDeviceID", AES256Cipher.AES_Encode("최종접속시디바이스ID-" + uuid, key));
				request.addProperty("lastIPaddress", AES256Cipher.AES_Encode("최종접속IP주소-" + uuid, key));
				request.addProperty("lastLoginDT", AES256Cipher.AES_Encode("UTC now", key));
				request.addProperty("lastLogoutDT", AES256Cipher.AES_Encode("UTC now", key));
				request.addProperty("lastMACAddress", AES256Cipher.AES_Encode("최종접속MAC주소-" + uuid, key));
				request.addProperty("hideYN", "N");//
				request.addProperty("deleteYN", "N");//
				request.addProperty("accountBlockYN", "N");//
				request.addProperty("accountBlockEndDT", "");//
				request.addProperty("anonymousYN", "N");//
				request.addProperty("sCol1", AES256Cipher.AES_Encode("여분의컬럼1-" + uuid, key));
				request.addProperty("sCol2", AES256Cipher.AES_Encode("여분의컬럼2-" + uuid, key));
				request.addProperty("sCol3", AES256Cipher.AES_Encode("여분의컬럼3-" + uuid, key));
				request.addProperty("sCol4", AES256Cipher.AES_Encode("여분의컬럼4-" + uuid, key));
				request.addProperty("sCol5", AES256Cipher.AES_Encode("여분의컬럼5-" + uuid, key));
				request.addProperty( "sCol6", AES256Cipher.AES_Encode("여분의컬럼6-" + uuid, key));
				request.addProperty("sCol7", AES256Cipher.AES_Encode("여분의컬럼7-" + uuid, key));
				request.addProperty("sCol8", AES256Cipher.AES_Encode("여분의컬럼8-" + uuid, key));
				request.addProperty("sCol9", AES256Cipher.AES_Encode("여분의컬럼9-" + uuid, key));
				request.addProperty("sCol10", AES256Cipher.AES_Encode("여분의컬럼10-" + uuid, key));
				request.addProperty("level", AES256Cipher.AES_Encode("1", key));
				request.addProperty("exps", AES256Cipher.AES_Encode("0", key));
				request.addProperty("points", AES256Cipher.AES_Encode("1000", key));
				request.addProperty("userSTAT1", AES256Cipher.AES_Encode("사용자상태속성1-" + uuid, key));
				request.addProperty("userSTAT2", AES256Cipher.AES_Encode("사용자상태속성2-" + uuid, key));
				request.addProperty("userSTAT3", AES256Cipher.AES_Encode("사용자상태속성3-" + uuid, key));
				request.addProperty("userSTAT4", AES256Cipher.AES_Encode("사용자상태속성5-" + uuid, key));
				request.addProperty("userSTAT5", AES256Cipher.AES_Encode("사용자상태속성5-" + uuid, key));
				request.addProperty("userSTAT6", AES256Cipher.AES_Encode("사용자상태속성6-" + uuid, key));
				request.addProperty("userSTAT7", AES256Cipher.AES_Encode("사용자상태속성7-" + uuid, key));
				request.addProperty("userSTAT8", AES256Cipher.AES_Encode("사용자상태속성8-" + uuid, key));
				request.addProperty("userSTAT9", AES256Cipher.AES_Encode("사용자상태속성9-" + uuid, key));
				request.addProperty("userSTAT10", AES256Cipher.AES_Encode("사용자상태속성10-" + uuid, key));
				request.addProperty("sCol1x", AES256Cipher.AES_Encode("여분의컬럼1-" + uuid, key));
				request.addProperty("sCol2x", AES256Cipher.AES_Encode("여분의컬럼2-" + uuid, key));
				request.addProperty("sCol3x", AES256Cipher.AES_Encode("여분의컬럼3-" + uuid, key));
				request.addProperty("sCol4x", AES256Cipher.AES_Encode("여분의컬럼4-" + uuid, key));
				request.addProperty("sCol5x", AES256Cipher.AES_Encode("여분의컬럼5-" + uuid, key));
				request.addProperty("sCol6x", AES256Cipher.AES_Encode("여분의컬럼6-" + uuid, key));
				request.addProperty("sCol7x", AES256Cipher.AES_Encode("여분의컬럼7-" + uuid, key));
				request.addProperty("sCol8x", AES256Cipher.AES_Encode("여분의컬럼8-" + uuid, key));
				request.addProperty("sCol9x", AES256Cipher.AES_Encode("여분의컬럼9-" + uuid, key));
				request.addProperty("sCol10x", AES256Cipher.AES_Encode("여분의컬럼10-" + uuid, key));
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		else
		{		
			request.addProperty("memberID", "회원ID-" + uuid);
			request.addProperty("memberPWD", "" + SHA_encrypt.encryption(uuid.toString()));//password는 SHA-512기본
			request.addProperty("emailAddress", "GUID@GUID.com");
			request.addProperty("emailConfirmedYN", "N");			
			request.addProperty("phoneNumber1", "전화번호1-" + uuid);
			request.addProperty("phoneNumber2", "전화번호2-" + uuid);
			request.addProperty("piNumber", "개인식별번호-" + uuid);
			request.addProperty("name1", "이름1-" + uuid);
			request.addProperty("name2", "이름2-" + uuid);
			request.addProperty("name3", "이름3-" + uuid);
			request.addProperty("dob", "19990101");
			request.addProperty("recommenderID", "추천인ID-" + uuid);
			request.addProperty("memberGroup", "회원그룹-" + uuid);
			request.addProperty("lastDeviceID", "최종접속시디바이스ID-" + uuid);
			request.addProperty("lastIPaddress", "최종접속IP주소-" + uuid);
			request.addProperty("lastLoginDT", "UTC now");
			request.addProperty("lastLogoutDT", "UTC now");
			request.addProperty("lastMACAddress", "최종접속MAC주소-" + uuid);
			request.addProperty("hideYN", "N");
			request.addProperty("deleteYN", "N");
			request.addProperty("accountBlockYN", "N");
			request.addProperty("accountBlockEndDT", "");
			request.addProperty("anonymousYN", "N");
			request.addProperty("sCol1", "여분의컬럼1-" + uuid);
			request.addProperty("sCol2", "여분의컬럼2-" + uuid);
			request.addProperty("sCol3", "여분의컬럼3-" + uuid);
			request.addProperty("sCol4", "여분의컬럼4-" + uuid);
			request.addProperty("sCol5", "여분의컬럼5-" + uuid);
			request.addProperty( "sCol6", "여분의컬럼6-" + uuid);
			request.addProperty("sCol7", "여분의컬럼7-" + uuid);
			request.addProperty("sCol8", "여분의컬럼8-" + uuid);
			request.addProperty("sCol9", "여분의컬럼9-" + uuid);
			request.addProperty("sCol10", "여분의컬럼10-" + uuid);
			request.addProperty("level", "1");
			request.addProperty("exps", "0");
			request.addProperty("points", "1000");
			request.addProperty("userSTAT1", "사용자상태속성1-" + uuid);
			request.addProperty("userSTAT2", "사용자상태속성2-" + uuid);
			request.addProperty("userSTAT3", "사용자상태속성3-" + uuid);
			request.addProperty("userSTAT4", "사용자상태속성5-" + uuid);
			request.addProperty("userSTAT5", "사용자상태속성5-" + uuid);
			request.addProperty("userSTAT6", "사용자상태속성6-" + uuid);
			request.addProperty("userSTAT7", "사용자상태속성7-" + uuid);
			request.addProperty("userSTAT8", "사용자상태속성8-" + uuid);
			request.addProperty("userSTAT9", "사용자상태속성9-" + uuid);
			request.addProperty("userSTAT10", "사용자상태속성10-" + uuid);
			request.addProperty("sCol1x", "여분의컬럼1-" + uuid);
			request.addProperty("sCol2x", "여분의컬럼2-" + uuid);
			request.addProperty("sCol3x", "여분의컬럼3-" + uuid);
			request.addProperty("sCol4x", "여분의컬럼4-" + uuid);
			request.addProperty("sCol5x", "여분의컬럼5-" + uuid);
			request.addProperty("sCol6x", "여분의컬럼6-" + uuid);
			request.addProperty("sCol7x", "여분의컬럼7-" + uuid);
			request.addProperty("sCol8x", "여분의컬럼8-" + uuid);
			request.addProperty("sCol9x", "여분의컬럼9-" + uuid);
			request.addProperty("sCol10x", "여분의컬럼10-" + uuid);
		}
		mText_request.setText(request.toString());
		mText_result.setText("");
		mClient.invokeApi("CBInsRegMember", request, new ApiJsonOperationCallback() {
		    
		    @Override
		    public void onCompleted(JsonElement result, Exception error,
		            ServiceFilterResponse response) {
		    	    if (error == null) {
		    	    	mText_result.setText(result.toString());
			        } else {
			        	mText_result.setText(error.getMessage());
			        }
		    }
		});
	}
	public void SelMemberItems(View view) {
		if (mClient == null) {
			return;
		}
		JsonObject request = new JsonObject();
		String key = "1234567890123456";
		if(mchk_encrytion.isChecked())
		{
			try{
				request.addProperty("memberID", AES256Cipher.AES_Encode("aaa", key));
				request.addProperty("page", AES256Cipher.AES_Encode("1", key));
				request.addProperty("pageSize", AES256Cipher.AES_Encode("5", key));
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		else
		{
			request.addProperty("memberID", "aaa");
			request.addProperty("page", "1");
			request.addProperty("pageSize", "5");		
		}
		mText_request.setText(request.toString());
		mText_result.setText("");
		mClient.invokeApi("CBSelMemberItems", request, new ApiJsonOperationCallback() {
		    
		    @Override
		    public void onCompleted(JsonElement result, Exception error,
		            ServiceFilterResponse response) {
		    	    if (error == null) {
		    	    	mText_result.setText(result.toString());
			        } else {
			        	mText_result.setText(error.getMessage());
			        }
		    }
		});
	}
	
	public void SelNotices(View view) {
		if (mClient == null) {
			return;
		}
	
		mText_request.setText("CBSelNotices");
		mText_result.setText("");
		mClient.invokeApi("CBSelNotices", null, new ApiJsonOperationCallback() {
		    
		    @Override
		    public void onCompleted(JsonElement result, Exception error,
		            ServiceFilterResponse response) {
		    	    if (error == null) {
		    	    	mText_result.setText(result.toString());
			        } else {
			        	mText_result.setText(error.getMessage());
			        }
		    }
		});
	}
	
	public void SelGameEvents(View view) {
		if (mClient == null) {
			return;
		}
		JsonObject request = new JsonObject();
		String key = "1234567890123456";
		if(mchk_encrytion.isChecked())
		{
			try{
				request.addProperty("memberID", AES256Cipher.AES_Encode("aaa", key));
				} catch (Exception e) {
				e.printStackTrace();
			}
		}
		else
		{
			request.addProperty("memberID", "aaa");
		}
		mText_request.setText(request.toString());
		mText_result.setText("");
		mClient.invokeApi("CBSelGameEvents", request, new ApiJsonOperationCallback() {
		    
		    @Override
		    public void onCompleted(JsonElement result, Exception error,
		            ServiceFilterResponse response) {
		    	    if (error == null) {
		    	    	mText_result.setText(result.toString());
			        } else {
			        	mText_result.setText(error.getMessage());
			        }
		    }
		});
	}
	
	public void SelCoupons(View view) {
		if (mClient == null) {
			return;
		}
		JsonObject request = new JsonObject();
		String key = "1234567890123456";
		if(mchk_encrytion.isChecked())
		{
			try{
				request.addProperty("memberID", AES256Cipher.AES_Encode("aaa", key));
				} catch (Exception e) {
				e.printStackTrace();
			}
		}
		else
		{
			request.addProperty("memberID", "aaa");
		}
		mText_request.setText(request.toString());
		mText_result.setText("");
		mClient.invokeApi("CBSelCoupons", request, new ApiJsonOperationCallback() {
		    
		    @Override
		    public void onCompleted(JsonElement result, Exception error,
		            ServiceFilterResponse response) {
		    	    if (error == null) {
		    	    	mText_result.setText(result.toString());
			        } else {
			        	mText_result.setText(error.getMessage());
			        }
		    }
		});
	}

	
	public void AddUseMemberItem_Insert(View view) {
		if (mClient == null) {
			return;
		}
		JsonObject request = new JsonObject();
		String key = "1234567890123456";
		UUID uuid = java.util.UUID.randomUUID();
		if(mchk_encrytion.isChecked())
		{
			try{				
			  request.addProperty("insertORUpdateORDelete", "INSERT");
			  request.addProperty("memberItemID_MemberItem", AES256Cipher.AES_Encode(uuid.toString(), key));
			  request.addProperty("memberID_MemberItem", AES256Cipher.AES_Encode("aaa", key));
			  request.addProperty("itemListID_MemberItem", AES256Cipher.AES_Encode("아이템4", key));
			  request.addProperty("itemCount_MemberItem", AES256Cipher.AES_Encode("10", key));
			  request.addProperty("itemStatus_MemberItem", AES256Cipher.AES_Encode("상태좋음", key));
			  request.addProperty("hideYN_MemberItem", AES256Cipher.AES_Encode("N", key));
			  request.addProperty("deleteYN_MemberItem", AES256Cipher.AES_Encode("N", key));
			  request.addProperty("sCol1_MemberItem", AES256Cipher.AES_Encode("여분의컬럼1-" + uuid, key));
			  request.addProperty("sCol2_MemberItem", AES256Cipher.AES_Encode("여분의컬럼2-" + uuid, key));
			  request.addProperty("sCol3_MemberItem", AES256Cipher.AES_Encode("여분의컬럼3-" + uuid, key));
			  request.addProperty("sCol4_MemberItem", AES256Cipher.AES_Encode("여분의컬럼4-" + uuid, key));
			  request.addProperty("sCol5_MemberItem", AES256Cipher.AES_Encode("여분의컬럼5-" + uuid, key));
			  request.addProperty("sCol6_MemberItem", AES256Cipher.AES_Encode("여분의컬럼6-" + uuid, key));
			  request.addProperty("sCol7_MemberItem", AES256Cipher.AES_Encode("여분의컬럼7-" + uuid, key));
			  request.addProperty("sCol8_MemberItem", AES256Cipher.AES_Encode("여분의컬럼8-" + uuid, key));
			  request.addProperty("sCol9_MemberItem", AES256Cipher.AES_Encode("여분의컬럼9-" + uuid, key));
			  request.addProperty("sCol10_MemberItem", AES256Cipher.AES_Encode("여분의컬럼10-" + uuid, key));
			  request.addProperty("memberID_MemberGameInfoes", AES256Cipher.AES_Encode("aaa", key));
			  request.addProperty("level_MemberGameInfoes", AES256Cipher.AES_Encode("2", key));
			  request.addProperty("exps_MemberGameInfoes", AES256Cipher.AES_Encode("20", key));
			  request.addProperty("points_MemberGameInfoes", AES256Cipher.AES_Encode("20", key));
			  request.addProperty("userSTAT1_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된상태값1-" + uuid, key));
			  request.addProperty("userSTAT2_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된상태값2-" + uuid, key));
			  request.addProperty("userSTAT3_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된상태값3-" + uuid, key));
			  request.addProperty("userSTAT4_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된상태값4-" + uuid, key));
			  request.addProperty("userSTAT5_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된상태값5-" + uuid, key));
			  request.addProperty("userSTAT6_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된상태값6-" + uuid, key));
			  request.addProperty("userSTAT7_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된상태값7-" + uuid, key));
			  request.addProperty("userSTAT8_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된상태값8-" + uuid, key));
			  request.addProperty("userSTAT9_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된상태값9-" + uuid, key));
			  request.addProperty("userSTAT10_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된상태값10-" + uuid, key));
			  request.addProperty("sCol1_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된여분의컬럼1-" + uuid, key));
			  request.addProperty("sCol2_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된여분의컬럼2-" + uuid, key));
			  request.addProperty("sCol3_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된여분의컬럼3-" + uuid, key));
			  request.addProperty("sCol4_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된여분의컬럼4-" + uuid, key));
			  request.addProperty("sCol5_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된여분의컬럼5-" + uuid, key));
			  request.addProperty("sCol6_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된여분의컬럼6-" + uuid, key));
			  request.addProperty("sCol7_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된여분의컬럼7-" + uuid, key));
			  request.addProperty("sCol8_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된여분의컬럼8-" + uuid, key));
			  request.addProperty("sCol9_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된여분의컬럼9-" + uuid, key));
			  request.addProperty("sCol10_MemberGameInfoes", AES256Cipher.AES_Encode("게임중변경된여분의컬럼10-" + uuid, key));
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		else
		{
			  request.addProperty("insertORUpdateORDelete", "INSERT");
			  request.addProperty("memberItemID_MemberItem", uuid.toString());
			  request.addProperty("memberID_MemberItem", "aaa");
			  request.addProperty("itemListID_MemberItem", "아이템4");
			  request.addProperty("itemCount_MemberItem", "10");
			  request.addProperty("itemStatus_MemberItem", "상태좋음");
			  request.addProperty("hideYN_MemberItem", "N");
			  request.addProperty("deleteYN_MemberItem", "N");
			  request.addProperty("sCol1_MemberItem", "여분의컬럼1-" + uuid);
			  request.addProperty("sCol2_MemberItem", "여분의컬럼2-" + uuid);
			  request.addProperty("sCol3_MemberItem", "여분의컬럼3-" + uuid);
			  request.addProperty("sCol4_MemberItem", "여분의컬럼4-" + uuid);
			  request.addProperty("sCol5_MemberItem", "여분의컬럼5-" + uuid);
			  request.addProperty("sCol6_MemberItem", "여분의컬럼6-" + uuid);
			  request.addProperty("sCol7_MemberItem", "여분의컬럼7-" + uuid);
			  request.addProperty("sCol8_MemberItem", "여분의컬럼8-" + uuid);
			  request.addProperty("sCol9_MemberItem", "여분의컬럼9-" + uuid);
			  request.addProperty("sCol10_MemberItem", "여분의컬럼10-" + uuid);
			  request.addProperty("memberID_MemberGameInfoes", "aaa");
			  request.addProperty("level_MemberGameInfoes", "2");
			  request.addProperty("exps_MemberGameInfoes", "20");
			  request.addProperty("points_MemberGameInfoes", "20");
			  request.addProperty("userSTAT1_MemberGameInfoes", "게임중변경된상태값1-" + uuid);
			  request.addProperty("userSTAT2_MemberGameInfoes", "게임중변경된상태값2-" + uuid);
			  request.addProperty("userSTAT3_MemberGameInfoes", "게임중변경된상태값3-" + uuid);
			  request.addProperty("userSTAT4_MemberGameInfoes", "게임중변경된상태값4-" + uuid);
			  request.addProperty("userSTAT5_MemberGameInfoes", "게임중변경된상태값5-" + uuid);
			  request.addProperty("userSTAT6_MemberGameInfoes", "게임중변경된상태값6-" + uuid);
			  request.addProperty("userSTAT7_MemberGameInfoes", "게임중변경된상태값7-" + uuid);
			  request.addProperty("userSTAT8_MemberGameInfoes", "게임중변경된상태값8-" + uuid);
			  request.addProperty("userSTAT9_MemberGameInfoes", "게임중변경된상태값9-" + uuid);
			  request.addProperty("userSTAT10_MemberGameInfoes", "게임중변경된상태값10-" + uuid);
			  request.addProperty("sCol1_MemberGameInfoes", "게임중변경된여분의컬럼1-" + uuid);
			  request.addProperty("sCol2_MemberGameInfoes", "게임중변경된여분의컬럼2-" + uuid);
			  request.addProperty("sCol3_MemberGameInfoes", "게임중변경된여분의컬럼3-" + uuid);
			  request.addProperty("sCol4_MemberGameInfoes", "게임중변경된여분의컬럼4-" + uuid);
			  request.addProperty("sCol5_MemberGameInfoes", "게임중변경된여분의컬럼5-" + uuid);
			  request.addProperty("sCol6_MemberGameInfoes", "게임중변경된여분의컬럼6-" + uuid);
			  request.addProperty("sCol7_MemberGameInfoes", "게임중변경된여분의컬럼7-" + uuid);
			  request.addProperty("sCol8_MemberGameInfoes", "게임중변경된여분의컬럼8-" + uuid);
			  request.addProperty("sCol9_MemberGameInfoes", "게임중변경된여분의컬럼9-" + uuid);
			  request.addProperty("sCol10_MemberGameInfoes", "게임중변경된여분의컬럼10-" + uuid);
		}
		mText_request.setText(request.toString());
		mText_result.setText("");
		mClient.invokeApi("CBAddUseMemberItem", request, new ApiJsonOperationCallback() {
		    
		    @Override
		    public void onCompleted(JsonElement result, Exception error,
		            ServiceFilterResponse response) {
		    	    if (error == null) {
		    	    	mText_result.setText(result.toString());
			        } else {
			        	mText_result.setText(error.getMessage());
			        }
		    }
		});
	}
	
	
	public void AddUseMemberItem_update(View view) {
		if (mClient == null) {
			return;
		}
		JsonObject request = new JsonObject();
		String key = "1234567890123456";
		UUID uuid = java.util.UUID.randomUUID();
		if(mchk_encrytion.isChecked())
		{
			try{				
			  request.addProperty("insertORUpdateORDelete", "UPDATE");
			  request.addProperty("memberItemID_MemberItem", AES256Cipher.AES_Encode("회원의아이템ID1-원래GUID값", key));
			  request.addProperty("memberID_MemberItem", AES256Cipher.AES_Encode("aaa", key));
			  request.addProperty("itemListID_MemberItem", AES256Cipher.AES_Encode("아이템4", key));
			  request.addProperty("itemCount_MemberItem", AES256Cipher.AES_Encode("999", key));
			  request.addProperty("itemStatus_MemberItem", AES256Cipher.AES_Encode("상태나쁨", key));
			  request.addProperty("hideYN_MemberItem", "N");
			  request.addProperty("deleteYN_MemberItem", "N");
			  request.addProperty("sCol1_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼1-" + uuid, key));
			  request.addProperty("sCol2_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼2-" + uuid, key));
			  request.addProperty("sCol3_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼3-" + uuid, key));
			  request.addProperty("sCol4_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼4-" + uuid, key));
			  request.addProperty("sCol5_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼5-" + uuid, key));
			  request.addProperty("sCol6_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼6-" + uuid, key));
			  request.addProperty("sCol7_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼7-" + uuid, key));
			  request.addProperty("sCol8_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼8-" + uuid, key));
			  request.addProperty("sCol9_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼9-" + uuid, key));
			  request.addProperty("sCol10_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼10-" + uuid, key));
			  request.addProperty("memberID_MemberGameInfoes", AES256Cipher.AES_Encode("aaa", key));
			  request.addProperty("level_MemberGameInfoes", AES256Cipher.AES_Encode("22", key));
			  request.addProperty("exps_MemberGameInfoes", AES256Cipher.AES_Encode("222", key));
			  request.addProperty("points_MemberGameInfoes", AES256Cipher.AES_Encode("222", key));
			  request.addProperty("userSTAT1_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값1-GUID");
			  request.addProperty("userSTAT2_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값2-GUID");
			  request.addProperty("userSTAT3_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값3-GUID");
			  request.addProperty("userSTAT4_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값4-GUID");
			  request.addProperty("userSTAT5_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값5-GUID");
			  request.addProperty("userSTAT6_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값6-GUID");
			  request.addProperty("userSTAT7_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값7-GUID");
			  request.addProperty("userSTAT8_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값8-GUID");
			  request.addProperty("userSTAT9_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값9-GUID");
			  request.addProperty("userSTAT10_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값10-GUID");
			  request.addProperty("sCol1_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼1-GUID");
			  request.addProperty("sCol2_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼2-GUID");
			  request.addProperty("sCol3_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼3-GUID");
			  request.addProperty("sCol4_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼4-GUID");
			  request.addProperty("sCol5_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼5-GUID");
			  request.addProperty("sCol6_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼6-GUID");
			  request.addProperty("sCol7_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼7-GUID");
			  request.addProperty("sCol8_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼8-GUID");
			  request.addProperty("sCol9_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼9-GUID");
			  request.addProperty("sCol10_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼10-GUID");
		    } catch (Exception e) {
				e.printStackTrace();
			}
		}
		else
		{
			  request.addProperty("insertORUpdateORDelete", "UPDATE");
			  request.addProperty("memberItemID_MemberItem", "회원의아이템ID1-원래GUID값");
			  request.addProperty("memberID_MemberItem", "aaa");
			  request.addProperty("itemListID_MemberItem", "아이템4");
			  request.addProperty("itemCount_MemberItem", "999");
			  request.addProperty("itemStatus_MemberItem", "상태나쁨");
			  request.addProperty("hideYN_MemberItem", "N");
			  request.addProperty("deleteYN_MemberItem", "N");
			  request.addProperty("sCol1_MemberItem", "변경된여분의컬럼1-" + uuid);
			  request.addProperty("sCol2_MemberItem", "변경된여분의컬럼2-" + uuid);
			  request.addProperty("sCol3_MemberItem", "변경된여분의컬럼3-" + uuid);
			  request.addProperty("sCol4_MemberItem", "변경된여분의컬럼4-" + uuid);
			  request.addProperty("sCol5_MemberItem", "변경된여분의컬럼5-" + uuid);
			  request.addProperty("sCol6_MemberItem", "변경된여분의컬럼6-" + uuid);
			  request.addProperty("sCol7_MemberItem", "변경된여분의컬럼7-" + uuid);
			  request.addProperty("sCol8_MemberItem", "변경된여분의컬럼8-" + uuid);
			  request.addProperty("sCol9_MemberItem", "변경된여분의컬럼9-" + uuid);
			  request.addProperty("sCol10_MemberItem", "변경된여분의컬럼10-" + uuid);
			  request.addProperty("memberID_MemberGameInfoes", "aaa");
			  request.addProperty("level_MemberGameInfoes", "22");
			  request.addProperty("exps_MemberGameInfoes", "222");
			  request.addProperty("points_MemberGameInfoes", "222");
			  request.addProperty("userSTAT1_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값1-GUID");
			  request.addProperty("userSTAT2_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값2-GUID");
			  request.addProperty("userSTAT3_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값3-GUID");
			  request.addProperty("userSTAT4_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값4-GUID");
			  request.addProperty("userSTAT5_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값5-GUID");
			  request.addProperty("userSTAT6_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값6-GUID");
			  request.addProperty("userSTAT7_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값7-GUID");
			  request.addProperty("userSTAT8_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값8-GUID");
			  request.addProperty("userSTAT9_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값9-GUID");
			  request.addProperty("userSTAT10_MemberGameInfoes", "다시UPDATE로변경된게임중변경된상태값10-GUID");
			  request.addProperty("sCol1_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼1-GUID");
			  request.addProperty("sCol2_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼2-GUID");
			  request.addProperty("sCol3_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼3-GUID");
			  request.addProperty("sCol4_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼4-GUID");
			  request.addProperty("sCol5_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼5-GUID");
			  request.addProperty("sCol6_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼6-GUID");
			  request.addProperty("sCol7_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼7-GUID");
			  request.addProperty("sCol8_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼8-GUID");
			  request.addProperty("sCol9_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼9-GUID");
			  request.addProperty("sCol10_MemberGameInfoes", "다시UPDATE로변경된게임중변경된여분의컬럼10-GUID");
		}
		mText_request.setText(request.toString());
		mText_result.setText("");
		mClient.invokeApi("CBAddUseMemberItem", request, new ApiJsonOperationCallback() {
		    
		    @Override
		    public void onCompleted(JsonElement result, Exception error,
		            ServiceFilterResponse response) {
		    	    if (error == null) {
		    	    	mText_result.setText(result.toString());
			        } else {
			        	mText_result.setText(error.getMessage());
			        }
		    }
		});
	}

	public void AddUseMemberItem_delete(View view) {
		if (mClient == null) {
			return;
		}
		JsonObject request = new JsonObject();
		String key = "1234567890123456";
		UUID uuid = java.util.UUID.randomUUID();
		if(mchk_encrytion.isChecked())
		{
			try{				
			  request.addProperty("insertORUpdateORDelete", "DELETE");
			  request.addProperty("memberItemID_MemberItem", AES256Cipher.AES_Encode("회원의아이템ID1-원래고유값", key));
//			  request.addProperty("memberID_MemberItem", AES256Cipher.AES_Encode("aaa", key));
//			  request.addProperty("itemListID_MemberItem", AES256Cipher.AES_Encode("아이템4", key));
//			  request.addProperty("itemCount_MemberItem", AES256Cipher.AES_Encode("999", key));
//			  request.addProperty("itemStatus_MemberItem", AES256Cipher.AES_Encode("상태나쁨", key));
//			  request.addProperty("hideYN_MemberItem", "N");
//			  request.addProperty("deleteYN_MemberItem", "N");
//			  request.addProperty("sCol1_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼1-" + uuid, key));
//			  request.addProperty("sCol2_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼2-" + uuid, key));
//			  request.addProperty("sCol3_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼3-" + uuid, key));
//			  request.addProperty("sCol4_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼4-" + uuid, key));
//			  request.addProperty("sCol5_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼5-" + uuid, key));
//			  request.addProperty("sCol6_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼6-" + uuid, key));
//			  request.addProperty("sCol7_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼7-" + uuid, key));
//			  request.addProperty("sCol8_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼8-" + uuid, key));
//			  request.addProperty("sCol9_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼9-" + uuid, key));
//			  request.addProperty("sCol10_MemberItem", AES256Cipher.AES_Encode("변경된여분의컬럼10-" + uuid, key));
			  request.addProperty("memberID_MemberGameInfoes", AES256Cipher.AES_Encode("aaa", key));
			  request.addProperty("level_MemberGameInfoes", AES256Cipher.AES_Encode("99", key));
			  request.addProperty("exps_MemberGameInfoes", AES256Cipher.AES_Encode("99", key));
			  request.addProperty("points_MemberGameInfoes", AES256Cipher.AES_Encode("999", key));
			  request.addProperty("userSTAT1_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값1-GUID");
			  request.addProperty("userSTAT2_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값2-GUID");
			  request.addProperty("userSTAT3_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값3-GUID");
			  request.addProperty("userSTAT4_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값4-GUID");
			  request.addProperty("userSTAT5_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값5-GUID");
			  request.addProperty("userSTAT6_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값6-GUID");
			  request.addProperty("userSTAT7_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값7-GUID");
			  request.addProperty("userSTAT8_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값8-GUID");
			  request.addProperty("userSTAT9_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값9-GUID");
			  request.addProperty("userSTAT10_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값10-GUID");
			  request.addProperty("sCol1_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼1-GUID");
			  request.addProperty("sCol2_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼2-GUID");
			  request.addProperty("sCol3_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼3-GUID");
			  request.addProperty("sCol4_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼4-GUID");
			  request.addProperty("sCol5_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼5-GUID");
			  request.addProperty("sCol6_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼6-GUID");
			  request.addProperty("sCol7_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼7-GUID");
			  request.addProperty("sCol8_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼8-GUID");
			  request.addProperty("sCol9_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼9-GUID");
			  request.addProperty("sCol10_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼10-GUID");
		    } catch (Exception e) {
				e.printStackTrace();
			}
		}
		else
		{
			  request.addProperty("insertORUpdateORDelete", "DELETE");
			  request.addProperty("memberItemID_MemberItem", "회원의아이템ID1-원래고유값");
//			  request.addProperty("memberID_MemberItem", "aaa");
//			  request.addProperty("itemListID_MemberItem", "아이템4");
//			  request.addProperty("itemCount_MemberItem", "999");
//			  request.addProperty("itemStatus_MemberItem", "상태나쁨");
//			  request.addProperty("hideYN_MemberItem", "N");
//			  request.addProperty("deleteYN_MemberItem", "N");
//			  request.addProperty("sCol1_MemberItem", "변경된여분의컬럼1-" + uuid);
//			  request.addProperty("sCol2_MemberItem", "변경된여분의컬럼2-" + uuid);
//			  request.addProperty("sCol3_MemberItem", "변경된여분의컬럼3-" + uuid);
//			  request.addProperty("sCol4_MemberItem", "변경된여분의컬럼4-" + uuid);
//			  request.addProperty("sCol5_MemberItem", "변경된여분의컬럼5-" + uuid);
//			  request.addProperty("sCol6_MemberItem", "변경된여분의컬럼6-" + uuid);
//			  request.addProperty("sCol7_MemberItem", "변경된여분의컬럼7-" + uuid);
//			  request.addProperty("sCol8_MemberItem", "변경된여분의컬럼8-" + uuid);
//			  request.addProperty("sCol9_MemberItem", "변경된여분의컬럼9-" + uuid);
//			  request.addProperty("sCol10_MemberItem", "변경된여분의컬럼10-" + uuid);
			  request.addProperty("memberID_MemberGameInfoes", "aaa");
			  request.addProperty("level_MemberGameInfoes", "22");
			  request.addProperty("exps_MemberGameInfoes", "222");
			  request.addProperty("points_MemberGameInfoes", "222");
			  request.addProperty("userSTAT1_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값1-GUID");
			  request.addProperty("userSTAT2_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값2-GUID");
			  request.addProperty("userSTAT3_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값3-GUID");
			  request.addProperty("userSTAT4_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값4-GUID");
			  request.addProperty("userSTAT5_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값5-GUID");
			  request.addProperty("userSTAT6_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값6-GUID");
			  request.addProperty("userSTAT7_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값7-GUID");
			  request.addProperty("userSTAT8_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값8-GUID");
			  request.addProperty("userSTAT9_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값9-GUID");
			  request.addProperty("userSTAT10_MemberGameInfoes", "다시DELETE로변경된게임중변경된상태값10-GUID");
			  request.addProperty("sCol1_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼1-GUID");
			  request.addProperty("sCol2_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼2-GUID");
			  request.addProperty("sCol3_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼3-GUID");
			  request.addProperty("sCol4_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼4-GUID");
			  request.addProperty("sCol5_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼5-GUID");
			  request.addProperty("sCol6_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼6-GUID");
			  request.addProperty("sCol7_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼7-GUID");
			  request.addProperty("sCol8_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼8-GUID");
			  request.addProperty("sCol9_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼9-GUID");
			  request.addProperty("sCol10_MemberGameInfoes", "다시DELETE로변경된게임중변경된여분의컬럼10-GUID");
		}
		mText_request.setText(request.toString());
		mText_result.setText("");
		mClient.invokeApi("CBAddUseMemberItem", request, new ApiJsonOperationCallback() {
		    
		    @Override
		    public void onCompleted(JsonElement result, Exception error,
		            ServiceFilterResponse response) {
		    	    if (error == null) {
		    	    	mText_result.setText(result.toString());
			        } else {
			        	mText_result.setText(error.getMessage());
			        }
		    }
		});
	}

	private class ProgressFilter implements ServiceFilter {
		
		@Override
		public void handleRequest(ServiceFilterRequest request, NextServiceFilterCallback nextServiceFilterCallback,
				final ServiceFilterResponseCallback responseCallback) {
			runOnUiThread(new Runnable() {

				@Override
				public void run() {
					if (mProgressBar != null) mProgressBar.setVisibility(ProgressBar.VISIBLE);
				}
			});
			
			nextServiceFilterCallback.onNext(request, new ServiceFilterResponseCallback() {
				
				@Override
				public void onResponse(ServiceFilterResponse response, Exception exception) {
					runOnUiThread(new Runnable() {

						@Override
						public void run() {
							if (mProgressBar != null) mProgressBar.setVisibility(ProgressBar.GONE);
						}
					});
					
					if (responseCallback != null)  responseCallback.onResponse(response, exception);
				}
			});
		}
	}
}
