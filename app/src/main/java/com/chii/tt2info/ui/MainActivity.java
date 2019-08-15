package com.chii.tt2info.ui;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.util.Log;
import android.view.View;
import android.support.v4.view.GravityCompat;
import android.support.v7.app.ActionBarDrawerToggle;
import android.view.MenuItem;
import android.support.design.widget.NavigationView;
import android.support.v4.widget.DrawerLayout;

import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.widget.AdapterView;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.VolleyError;
import com.chii.tt2info.R;
import com.chii.tt2info.adapter.ListViewAdapter;
import com.chii.tt2info.connes.MyVolley;
import com.chii.tt2info.connes.volleyInterface;
import com.chii.tt2info.pojo.Info;
import com.chii.tt2info.util.SPUtil;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

import static com.chii.tt2info.connes.MyVolley.infolist_url;

public class MainActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {
    @BindView(R.id.toolbar)
    Toolbar toolbar;
    @BindView(R.id.fab)
    FloatingActionButton fab;
    @BindView(R.id.drawer_layout)
    DrawerLayout drawer;
    @BindView(R.id.nav_view)
    NavigationView navigationView;
    @BindView(R.id.mainlistview)
    ListView listView;

    TextView signinState;
    ImageView headImage;
    private ListViewAdapter mAdapter;
    private Context context = this;
    private Gson gson = new Gson();
    private Boolean isSignin = false;
    List<Info> infoList = new ArrayList<>();
    public static String TAG = "MainActivitytag";
    MyVolley myVolley;
    public final static int REQUESTCODE_FROM_LOGIN = 1;
    public final static int REQUESTCODE_FROM_REGISTER = 2;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        ButterKnife.bind(this);
        setSupportActionBar(toolbar);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open,
                R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();
        navigationView.setNavigationItemSelectedListener(this);
        myVolley = MyVolley.getMyVolley(MainActivity.this);

        View headerView = navigationView.getHeaderView(0);
        signinState = headerView.findViewById(R.id.signinState);
        headImage = headerView.findViewById(R.id.headImage);
        signinState.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if (isSignin != null && isSignin) {
                    SPUtil.put(MainActivity.this, "isSignin", false);
                }
                Intent intent = new Intent(MainActivity.this, LoginActivity.class);
                startActivityForResult(intent, REQUESTCODE_FROM_LOGIN);
                logged(false);
                Toast.makeText(context, "hello", Toast.LENGTH_LONG).show();
            }
        });
        initList();
        initDate();

    }

    public void initDate() {
        HashMap<String, String> map = new HashMap<String, String>();
        String saveusername = (String) SPUtil.get(MainActivity.this, "username", "");
        String savepasswd = (String) SPUtil.get(MainActivity.this, "passwd", "");
        SPUtil.put(MainActivity.this, "isSignin", true);
        isSignin = (Boolean) SPUtil.get(MainActivity.this, "isSignin", Boolean.FALSE);

        Log.d(TAG, "initDate: " + saveusername + " " + savepasswd + " " + isSignin);
        if (isSignin != null && isSignin) {
            map.put("username", saveusername);
            logged(true);//已经登录处理
            getinfoList(map);
        }

    }

    private void getinfoList(HashMap<String, String> map) {
        myVolley.Get(infolist_url, map, new volleyInterface() {
            @Override
            public void ResponseResult(String jsonObject) {
                Type type = new TypeToken<List<Info>>() {
                }.getType();
                List<Info> list = gson.fromJson(jsonObject, type);
                Collections.reverse(list);//将list逆序
                infoList.addAll(list);
                mAdapter.notifyDataSetChanged();
            }

            @Override
            public void ResponError(VolleyError volleyError) {
                Log.d(TAG, "GET请求失败" + volleyError.toString());
            }
        });
    }


    private void logged(Boolean flag) {
        if (flag) {
            signinState.setText("退出登录");
            headImage.setImageDrawable(getResources().getDrawable((R.drawable.ic_check_black),
                    null));
        } else {
            signinState.setText("点击登录");
            headImage.setImageDrawable(getResources().getDrawable((R.drawable.ic_close_black),
                    null));
        }
    }

    public void initList() {
        Log.d(TAG, "initList: test");
        mAdapter = new ListViewAdapter(this, infoList);
        listView.setAdapter(mAdapter);
        listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Toast.makeText(context, "hello" + position, Toast.LENGTH_LONG).show();
                Intent intent = new Intent();
                intent.putExtra("mid", infoList.get(position).getMid());
                intent.setClass(MainActivity.this, InfosActivity.class);
                MainActivity.this.startActivity(intent);
            }
        });
        listView.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {
            @Override
            public boolean onItemLongClick(AdapterView<?> parent, View view, int position,
                                           long id) {
                Toast.makeText(context, "OnItemLongClickListener", Toast.LENGTH_SHORT).show();
                return true;
            }
        });

    }

    @OnClick(R.id.fab)
    public void fabClick(View view) {
        infoList.clear();
        initDate();
        Snackbar.make(view, "正在更新数据ing", Snackbar.LENGTH_SHORT)
                .setAction("Action", null).show();
    }


    @Override
    public void onBackPressed() {
        DrawerLayout drawer = findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_home) {

        } else if (id == R.id.nav_daynight) {

        } else if (id == R.id.nav_settings) {

        }

        DrawerLayout drawer = findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        // 当otherActivity中返回数据的时候，会响应此方法
        // requestCode和resultCode必须与请求startActivityForResult()和返回setResult()的时候传入的值一致。
        Log.d(TAG, "onActivityResult: " + requestCode + "  " + resultCode);
        Log.d(TAG, "RESULT_OK " + Activity.RESULT_OK);

        switch (requestCode) {
            case REQUESTCODE_FROM_LOGIN:
                if (resultCode == Activity.RESULT_OK) {
                    Log.d(TAG, "LoginActivity.RESULT_CODE ");
                    infoList.clear();
                    initDate();
                }
                break;
            default:
                break;
        }

    }

}
