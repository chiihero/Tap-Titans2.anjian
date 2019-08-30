package com.chii.tt2info.ui.activity;

import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.res.Configuration;
import android.os.Bundle;

import androidx.annotation.Nullable;

import com.chii.tt2info.ui.fragment.SettingsFragment;
import com.google.android.material.floatingactionbutton.FloatingActionButton;
import com.google.android.material.snackbar.Snackbar;

import android.os.Environment;
import android.util.Log;
import android.view.View;

import androidx.appcompat.app.AppCompatDelegate;
import androidx.appcompat.widget.AppCompatImageView;
import androidx.core.view.GravityCompat;
import androidx.appcompat.app.ActionBarDrawerToggle;

import android.view.MenuItem;

import com.google.android.material.navigation.NavigationView;

import androidx.drawerlayout.widget.DrawerLayout;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;

import android.view.Menu;
import android.widget.AdapterView;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.VolleyError;
import com.chii.tt2info.R;
import com.chii.tt2info.ui.adapter.ListViewAdapter;
import com.chii.tt2info.connes.MyVolley;
import com.chii.tt2info.connes.volleyInterface;
import com.chii.tt2info.pojo.Info;
import com.chii.tt2info.pojo.Page;
import com.chii.tt2info.util.SPUtil;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.io.File;
import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;

import static com.chii.tt2info.connes.MyVolley.infodeleteAll_url;
import static com.chii.tt2info.connes.MyVolley.infopage_url;

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
    private MyVolley myVolley;
    public final static int REQUESTCODE_FROM_LOGIN = 1;
    public final static int REQUESTCODE_FROM_REGISTER = 2;
    private static String pageNum = "1";
    private static String pageSize = "100";
    private AlertDialog.Builder builder;
    private String saveusername, savepasswd;

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
//        upThemeVw();

        myVolley = MyVolley.getMyVolley(MainActivity.this);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                infoList.clear();
                initDate();
                Snackbar.make(view, "正在更新数据ing", Snackbar.LENGTH_SHORT)
                        .setAction("Action", null).show();
            }
        });
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
        saveusername = (String) SPUtil.get(MainActivity.this, "username", "");
        savepasswd = (String) SPUtil.get(MainActivity.this, "passwd", "");
        SPUtil.put(MainActivity.this, "isSignin", true);
        isSignin = (Boolean) SPUtil.get(MainActivity.this, "isSignin", Boolean.FALSE);

        Log.d(TAG, "initDate: " + saveusername + " " + savepasswd + " " + isSignin);
        if (isSignin != null && isSignin) {
            Log.d(TAG, "isSignin: " + isSignin);
            map.put("username", saveusername);
            map.put("pageNum", pageNum);
            map.put("pageSize", pageSize);
            logged(true);//已经登录处理
            getinfoList(map);
        }

    }

    private void getinfoList(HashMap<String, String> map) {
        myVolley.Get(infopage_url, map, new volleyInterface() {
            @Override
            public void ResponseResult(String jsonObject) {
                Type type = new TypeToken<Page<Info>>() {
                }.getType();
                Page<Info> list = gson.fromJson(jsonObject, type);
                infoList.addAll(list.getList());
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
        switch (id) {
            case R.id.action_show_100:
                Log.d(TAG, "onOptionsItemSelected: action_show_100");
                pageSize = "100";
                break;
            case R.id.action_show_1000:
                Log.d(TAG, "onOptionsItemSelected: action_show_1000");
                pageSize = "1000";
                break;
            case R.id.action_show_all:
                Log.d(TAG, "onOptionsItemSelected: action_show_all");
                pageSize = "0";
                break;
            case R.id.action_clear:
                Log.d(TAG, "onOptionsItemSelected: action_clear");
                showDialog();
                return true;
        }
        infoList.clear();
        initDate();
        return super.onOptionsItemSelected(item);
    }

    /**
     * dialog
     */
    private void showDialog() {
        builder = new AlertDialog.Builder(this).setIcon(R.drawable.ic_delete_sweep).setTitle("删除信息")
                .setMessage("你要删除哪些数据？").setNeutralButton("全部", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        HashMap<String, String> map = new HashMap<String, String>();
                        map.put("username", saveusername);
                        map.put("passwd", savepasswd);
                        myVolley.Post(infodeleteAll_url, map, new volleyInterface() {
                            @Override
                            public void ResponseResult(String jsonObject) {
                                if (jsonObject.equals("true")) {
                                    Toast.makeText(MainActivity.this, "成功", Toast.LENGTH_LONG).show();
                                } else {
                                    Toast.makeText(MainActivity.this, "失败", Toast.LENGTH_LONG).show();
                                }
                            }
                            @Override
                            public void ResponError(VolleyError volleyError) {
                            }
                        });
                    }
                    //ToDo: 你想做的事情
                }).setNegativeButton("一个月前(无效)", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        Toast.makeText(MainActivity.this, "一个月前", Toast.LENGTH_LONG).show();
                    }
                }).setPositiveButton("取消", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        Toast.makeText(MainActivity.this, "取消", Toast.LENGTH_LONG).show();
                        dialogInterface.dismiss();
                    }
                });

        builder.create().show();
    }

    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.

        int id = item.getItemId();
        switch (id) {
            case R.id.nav_home:

                break;
            case R.id.nav_daynight:
                upThemeVw();
//
                break;
            case R.id.nav_settings:
                Intent intent = new Intent(MainActivity.this, SettingActivity.class);
                startActivity(intent);
                break;

        }


        DrawerLayout drawer = findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }
    /**
     * 更新主题切换按钮
     */
    private void upThemeVw() {

        Menu drawerMenu = navigationView.getMenu();

        MenuItem vwNightTheme = drawerMenu.findItem(R.id.nav_daynight);
        int mode = getResources().getConfiguration().uiMode & Configuration.UI_MODE_NIGHT_MASK;
        if (mode == Configuration.UI_MODE_NIGHT_YES) {
            AppCompatDelegate.setDefaultNightMode(AppCompatDelegate.MODE_NIGHT_NO);
//            vwNightTheme.setIcon(R.drawable.ic_theme_night);
//            vwNightTheme.setTitle("nigth");
        } else if (mode == Configuration.UI_MODE_NIGHT_NO) {
            AppCompatDelegate.setDefaultNightMode(AppCompatDelegate.MODE_NIGHT_YES);
//            vwNightTheme.setIcon(R.drawable.ic_theme_day);
//            vwNightTheme.setTitle("day");
        }
        recreate();
        if (mode == Configuration.UI_MODE_NIGHT_YES) {
            vwNightTheme.setIcon(R.drawable.ic_theme_night);
            vwNightTheme.setTitle("nigth");
        } else if (mode == Configuration.UI_MODE_NIGHT_NO) {
            vwNightTheme.setIcon(R.drawable.ic_theme_day);
            vwNightTheme.setTitle("day");
        }
//        finish();
//                startActivity(new Intent( this, this.getClass()));
//                overridePendingTransition(0, 0);
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
