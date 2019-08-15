package com.chii.tt2info.ui;

import android.content.Context;
import android.content.Intent;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.MenuItem;
import android.widget.ListView;

import com.android.volley.VolleyError;
import com.chii.tt2info.R;
import com.chii.tt2info.adapter.InfosListViewAdapter;
import com.chii.tt2info.connes.MyVolley;
import com.chii.tt2info.connes.volleyInterface;
import com.chii.tt2info.pojo.Infos;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;

import static com.chii.tt2info.connes.MyVolley.infos_url;

public class InfosActivity extends AppCompatActivity {
    @BindView(R.id.infolistview)
    ListView listView;

    public static String TAG = "InfosActivitytag";
    MyVolley myVolley;
    private InfosListViewAdapter mAdapter;
    private Context context = this;
    private Gson gson = new Gson();
    List<Infos> infosList = new ArrayList<>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_infos);
        ButterKnife.bind(this);

        ActionBar actionBar = getSupportActionBar();
        if(actionBar != null){
            actionBar.setHomeButtonEnabled(true);
            actionBar.setDisplayHomeAsUpEnabled(true);
        }


        Intent intent = getIntent();
        int mid = intent.getIntExtra("mid",0);
        Log.d(TAG, "onCreate: "+mid);
        initList();
        initDate(mid);

    }

    private void initDate(int mid) {
        myVolley = MyVolley.getMyVolley(InfosActivity.this);
        HashMap<String, String> map = new HashMap<String, String>();
        map.put("mid", String.valueOf(mid));
        myVolley.Get(infos_url, map, new volleyInterface() {

            @Override
            public void ResponseResult(String jsonObject) {
                Type type = new TypeToken<List<Infos>>() {
                }.getType();
                List<Infos> list = gson.fromJson(jsonObject, type);
                infosList.addAll(list);
                Log.d(TAG, "ResponseResult: " + infosList.get(0).getLayer());
                mAdapter.notifyDataSetChanged();
            }

            @Override
            public void ResponError(VolleyError volleyError) {
                Log.d(TAG, "GET请求失败" + volleyError.toString());
            }
        });
    }

    private void initList() {
        mAdapter = new InfosListViewAdapter(this,infosList);
        listView.setAdapter(mAdapter);

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
