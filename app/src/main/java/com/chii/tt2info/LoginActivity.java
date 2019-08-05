package com.chii.tt2info;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;

import com.android.volley.VolleyError;
import com.chii.tt2info.connes.MyVolley;
import com.chii.tt2info.connes.volleyInterface;
import com.chii.tt2info.pojo.User;
import com.chii.tt2info.util.SPUtil;
import com.google.gson.Gson;

import java.util.HashMap;

import butterknife.BindView;
import butterknife.ButterKnife;

import static com.chii.tt2info.connes.MyVolley.login_url;

public class LoginActivity extends AppCompatActivity {

    @BindView(R.id.username)
    EditText usernameEditText;
    @BindView(R.id.password)
    EditText passwordEditText;
    @BindView(R.id.login)
    Button loginButton;
    @BindView(R.id.loading)
    ProgressBar loadingProgressBar;
    MyVolley myVolley;
    private Gson gson = new Gson();
    User user = new User();
    public static String TAG = "LoginActivity";
    private String username, passwd;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        ButterKnife.bind(this);
        myVolley = MyVolley.getMyVolley(LoginActivity.this);


        String saveusername = (String) SPUtil.get(LoginActivity.this, "username", "");
        String savepasswd = (String) SPUtil.get(LoginActivity.this, "passwd", "");
        Log.d(TAG, "onCreate: " + saveusername + " " + savepasswd);
        if ((!savepasswd.equals("")) && (!saveusername.equals(""))) {
            initDate(saveusername, savepasswd);
        }

        loginButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                loadingProgressBar.setVisibility(View.VISIBLE);
                String username = usernameEditText.getText().toString();
                String passwd = passwordEditText.getText().toString();
                initDate(username, passwd);
            }
        });

    }

    private void initDate(final String username, final String passwd) {
        HashMap<String, String> map = new HashMap<String, String>();
        map.put("username", username);
        map.put("passwd", passwd);
        myVolley.Post(login_url, map, new volleyInterface() {
            @Override
            public void ResponseResult(String jsonObject) {
                Log.d(TAG, "ResponseResult: " + jsonObject);
                if (jsonObject.equals("")) {
                    Log.d(TAG, "登录失败");
                } else {
                    Log.d(TAG, "登录成功");
                    SPUtil.put(LoginActivity.this, "username", username);
                    SPUtil.put(LoginActivity.this, "passwd", passwd);
                    finish();
                }
            }

            @Override
            public void ResponError(VolleyError volleyError) {

            }
        });
    }


}
