package com.chii.tt2info.adapter;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import android.widget.Toast;


import com.chii.tt2info.MainActivity;
import com.chii.tt2info.R;
import com.chii.tt2info.pojo.Info;
import com.chii.tt2info.swipe.SimpleSwipeListener;
import com.chii.tt2info.swipe.SwipeLayout;
import com.chii.tt2info.swipe.adapters.BaseSwipeAdapter;
import com.chii.tt2info.util.TimeUtil;

import java.util.List;

public class ListViewAdapter extends BaseSwipeAdapter {

    private Context mContext;
    private List<Info> minfolist;

    public ListViewAdapter(Context mContext, List<Info> infolist) {
        this.mContext = mContext;
        this.minfolist = infolist;
    }

    @Override
    public int getSwipeLayoutResourceId(int position) {
        return R.id.swipe;
    }

    @Override
    public View generateView(int position, ViewGroup parent) {
        View v = LayoutInflater.from(mContext).inflate(R.layout.listview_list, null);
        SwipeLayout swipeLayout = (SwipeLayout)v.findViewById(getSwipeLayoutResourceId(position));
        swipeLayout.addSwipeListener(new SimpleSwipeListener() {
            @Override
            public void onOpen(SwipeLayout layout) {

            }
        });

        v.findViewById(R.id.delete).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Toast.makeText(mContext, "click delete", Toast.LENGTH_SHORT).show();
            }
        });
        return v;
    }

    @Override
    public void fillValues(int position, View convertView) {
        TextView TV_title = (TextView)convertView.findViewById(R.id.info_title);
        TextView TV_notes = (TextView)convertView.findViewById(R.id.info_notes);
        TextView TV_layerSet = (TextView)convertView.findViewById(R.id.info_layerSet);
        TextView TV_updateAll = (TextView)convertView.findViewById(R.id.info_updateAll);
        TextView TV_updateMini = (TextView)convertView.findViewById(R.id.info_updateMini);
        TextView TV_time = (TextView)convertView.findViewById(R.id.info_time);

        String title =minfolist.get(position).getTitle();
        String layerSet = "层数设定："+minfolist.get(position).getLayerSet().toString();
        String notes ="备注："+minfolist.get(position).getNotes();
        String updateAll = "全面升级次数："+minfolist.get(position).getUpdateAll().toString();
        String updateMini = "小升级次数："+minfolist.get(position).getUpdateMini().toString();
        String timestamp = minfolist.get(position).getTime();
        //2019-08-05 09:22:27.0|获取.之前的字符串
        String time = TimeUtil.String2Data(timestamp).toString().split("\\.")[0];

        TV_title.setText(title);
        TV_notes.setText(notes);
        TV_layerSet.setText(layerSet);
        TV_updateAll.setText(updateAll);
        TV_updateMini.setText(updateMini);
        TV_time.setText(time);

    }

    @Override
    public int getCount() {
        return minfolist.size();
    }

    @Override
    public Object getItem(int position) {
        return minfolist.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }
}
