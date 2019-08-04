package com.chii.tt2info;

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

import java.util.List;

public class ListViewAdapter extends BaseSwipeAdapter {

    private Context mContext;
    private List<Info> minfolist;

    ListViewAdapter(Context mContext, List<Info> infolist ) {
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
        TextView tpos = (TextView)convertView.findViewById(R.id.position);
        TextView tdata = (TextView)convertView.findViewById(R.id.text_data);

        tpos.setText(String.valueOf(position + 1));
        String title = minfolist.get(position).getLayerSet().toString();
        Log.d(MainActivity.TAG, "fillValues: "+title);
        tdata.setText(title);
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
