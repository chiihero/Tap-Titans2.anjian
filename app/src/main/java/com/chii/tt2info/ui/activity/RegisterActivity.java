package com.chii.tt2info.ui.activity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;

import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AppCompatActivity;

import android.util.Log;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.VolleyError;
import com.chii.tt2info.R;
import com.chii.tt2info.connes.MyVolley;
import com.chii.tt2info.connes.volleyInterface;
import com.chii.tt2info.pojo.User;
import com.chii.tt2info.util.Md5;
import com.google.gson.Gson;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;

import butterknife.BindView;
import butterknife.ButterKnife;

import static com.chii.tt2info.connes.MyVolley.register_url;

public class RegisterActivity extends AppCompatActivity {
    @BindView(R.id.register_imageView)
    ImageView register_imageView;
    @BindView(R.id.register_morning)
    TextView register_morning;
    @BindView(R.id.register_username)
    EditText usernameEditText;
    @BindView(R.id.register_email)
    EditText emailEditText;
    @BindView(R.id.register_passwd)
    EditText passwordEditText;
    @BindView(R.id.register)
    Button registerButton;

    MyVolley myVolley;
    private Gson gson = new Gson();
    User user = new User();
    public static String TAG = "RegisterActivity";

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);
        ButterKnife.bind(this);
        myVolley = MyVolley.getMyVolley(RegisterActivity.this);
        ActionBar actionBar = getSupportActionBar();
        if (actionBar != null) {
            actionBar.setHomeButtonEnabled(true);
            actionBar.setDisplayHomeAsUpEnabled(true);
        }
        if (getCurrentTime()){
            Toast.makeText(this,"晚上",Toast.LENGTH_SHORT).show();
            register_imageView.setImageResource(R.drawable.good_night_img);
            register_morning.setText("Night");
        }else {
            Toast.makeText(this,"白天",Toast.LENGTH_SHORT).show();
            register_imageView.setImageResource(R.drawable.good_morning_img);
        }
        registerButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String username = usernameEditText.getText().toString();
                String email = emailEditText.getText().toString();
                String passwd = passwordEditText.getText().toString();
                passwd = Md5.safepasswd(passwd, 1024);
                initDate(username, email, passwd);
            }
        });

    }
    public boolean getCurrentTime(){
        SimpleDateFormat sdf = new SimpleDateFormat("HH");
        String hour= sdf.format(new Date());
        int k  = Integer.parseInt(hour)  ;
        return (k >= 0 && k < 6) || (k >= 18 && k < 24);
    }

    private void initDate(final String username, final String email, final String passwd) {
        HashMap<String, String> map = new HashMap<String, String>();
        map.put("username", username);
        map.put("email", email);
        map.put("passwd", passwd);
        myVolley.Post(register_url, map, new volleyInterface() {
            @Override
            public void ResponseResult(String jsonObject) {
                Log.d(TAG, "ResponseResult: " + jsonObject);
                if (jsonObject.equals("false")) {
                    Log.d(TAG, "注册失败");
                } else if (jsonObject.equals("true")) {
                    Log.d(TAG, "注册成功");
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
