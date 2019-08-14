package com.chii.tt2info;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;

import com.android.volley.VolleyError;
import com.chii.tt2info.connes.MyVolley;
import com.chii.tt2info.connes.volleyInterface;
import com.chii.tt2info.pojo.User;
import com.chii.tt2info.util.Md5;
import com.chii.tt2info.util.SPUtil;
import com.google.gson.Gson;

import java.util.HashMap;

import butterknife.BindView;
import butterknife.ButterKnife;

import static com.chii.tt2info.MainActivity.REQUESTCODE_FROM_REGISTER;
import static com.chii.tt2info.connes.MyVolley.signin_url;

public class LoginActivity extends AppCompatActivity {

    @BindView(R.id.username)
    EditText usernameEditText;
    @BindView(R.id.password)
    EditText passwordEditText;
    @BindView(R.id.toregister)
    Button toregisterButton;
    @BindView(R.id.login)
    Button loginButton;
    @BindView(R.id.loading)
    ProgressBar loadingProgressBar;
    MyVolley myVolley;
    private Gson gson = new Gson();
    User user = new User();
    public static String TAG = "LoginActivity";
    private String username, passwd;
    Boolean isSignin = false;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        ButterKnife.bind(this);
        myVolley = MyVolley.getMyVolley(LoginActivity.this);
        ActionBar actionBar = getSupportActionBar();
        if (actionBar != null) {
            actionBar.setHomeButtonEnabled(true);
            actionBar.setDisplayHomeAsUpEnabled(true);
        }

        String saveusername = (String) SPUtil.get(LoginActivity.this, "username", "");
        String savepasswd = (String) SPUtil.get(LoginActivity.this, "passwd", "");
        isSignin = (Boolean) SPUtil.get(LoginActivity.this, "isSignin", Boolean.FALSE);
        Log.d(TAG, "onCreate: " + saveusername + " " + savepasswd);
        if (isSignin != null && isSignin) {
            initDate(saveusername, savepasswd);
        } else if (saveusername != null && savepasswd != null) {
            usernameEditText.setText(saveusername);
            passwordEditText.setText(savepasswd);
        }

        loginButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                loadingProgressBar.setVisibility(View.VISIBLE);
                String username = usernameEditText.getText().toString();
                String passwd = passwordEditText.getText().toString();
                passwd = Md5.safepasswd(passwd,1024);
                initDate(username, passwd);
            }
        });
        toregisterButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(LoginActivity.this, RegisterActivity.class);
                startActivityForResult(intent, REQUESTCODE_FROM_REGISTER);
            }
        });

    }

    private void initDate(final String username, final String passwd) {
        Log.d(TAG, "initDate: "+passwd);
        HashMap<String, String> map = new HashMap<String, String>();
        map.put("username", username);
        map.put("passwd", passwd);
        myVolley.Post(signin_url, map, new volleyInterface() {
            @Override
            public void ResponseResult(String jsonObject) {
                Log.d(TAG, "ResponseResult: " + jsonObject);
                if (jsonObject.equals("")) {
                    Log.d(TAG, "登录失败");
                    loadingProgressBar.setVisibility(View.GONE);

                } else {
                    Log.d(TAG, "登录成功");
                    SPUtil.put(LoginActivity.this, "isSignin", true);
                    SPUtil.put(LoginActivity.this, "username", username);
                    SPUtil.put(LoginActivity.this, "passwd", passwd);
                    Intent intent = new Intent();
                    intent.putExtra("username", username);
                    intent.putExtra("passwd", passwd);
                    setResult(Activity.RESULT_OK, intent);
                    finish();

                }
            }

            @Override
            public void ResponError(VolleyError volleyError) {

            }
        });
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        // 当otherActivity中返回数据的时候，会响应此方法
        // requestCode和resultCode必须与请求startActivityForResult()和返回setResult()的时候传入的值一致。
        Log.d(TAG, "onActivityResult: " + requestCode + "  " + resultCode);
        Log.d(TAG, "RESULT_OK " + Activity.RESULT_OK);
        switch (requestCode) {
            case REQUESTCODE_FROM_REGISTER:
                if (resultCode == Activity.RESULT_OK) {
                    Log.d(TAG, "RegisterActivity.REQUESTCODE_FROM_REGISTER ");
                    String username = data.getStringExtra("username");
                    String passwd = data.getStringExtra("passwd");

                    Log.d(TAG, "onCreate: " + username + " " + passwd);
                    if ((!passwd.equals("")) && (!username.equals(""))) {
                        initDate(username, passwd);
                    }
                }
                break;
            default:
                break;
        }

    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                finish();
                break;

            default:
                break;
        }
        return super.onOptionsItemSelected(item);
    }
}
