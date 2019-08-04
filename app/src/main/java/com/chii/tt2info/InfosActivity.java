package com.chii.tt2info;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.ListView;

import butterknife.BindView;
import butterknife.ButterKnife;

public class InfosActivity extends AppCompatActivity {


    @BindView(R.id.infolistview)
    ListView listView;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_infos);
        ButterKnife.bind(this);


        initList();

    }

    private void initList() {



    }
}
