package com.chii.tt2info;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;


import com.chii.tt2info.pojo.Info;


import java.util.List;

import javax.security.auth.callback.Callback;

public class ListViewAdapter extends BaseAdapter {

    private Context mContext;
    private List<Info> minfolist;
    private Callback mCallback;

    ListViewAdapter(Context mContext, List<Info> infolist, Callback callback) {
        this.mContext = mContext;
        this.minfolist = infolist;
        this.mCallback = callback;
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

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        if (convertView == null) {
            convertView = LayoutInflater.from(mContext).inflate(R.layout.listview_list, null);
        }
        String pos = String.valueOf(position + 1) + ".";
        String title = minfolist.get(position).getLayerSet().toString();

        TextView tpos = ViewHolder.get(convertView, R.id.position);
        TextView tdata = ViewHolder.get(convertView, R.id.text_data);
        TextView delete = ViewHolder.get(convertView, R.id.delete);

        SwipeListLayout swipeListLayout = ViewHolder.get(convertView, R.id.swipe);

        swipeListLayout.setOnSwipeStatusListener(new MyOnSlipStatusListener(swipeListLayout));
        tpos.setText(pos);
        tdata.setText(title);
        delete.setTag(position);
        delete.setOnClickListener(this);
        return convertView;
    }

    public interface Callback {
        public void click(View v);
    }
    //响应按钮点击事件,调用子定义接口，并传入View
    @Override
    public void onClick(View v) {
        mCallback.click(v);
    }
}
