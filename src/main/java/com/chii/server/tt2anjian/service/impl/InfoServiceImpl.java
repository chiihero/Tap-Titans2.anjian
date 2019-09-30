package com.chii.server.tt2anjian.service.impl;

import com.chii.server.tt2anjian.mapper.InfoMapper;
import com.chii.server.tt2anjian.mapper.UserMapper;
import com.chii.server.tt2anjian.pojo.Info;
import com.chii.server.tt2anjian.pojo.PageBean;
import com.chii.server.tt2anjian.pojo.User;
import com.chii.server.tt2anjian.service.InfoService;
import com.chii.server.tt2anjian.service.UserService;
import com.github.pagehelper.PageHelper;
import com.github.pagehelper.PageInfo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class InfoServiceImpl implements InfoService {
    private final InfoMapper infoMapper;

    @Autowired
    public InfoServiceImpl(InfoMapper infoMapper) {
        this.infoMapper = infoMapper;
    }

    @Override
    public PageInfo<Info> getInfoInfoByUsername(PageBean pageBean) {
        PageHelper.startPage(pageBean.getCurrent(), pageBean.getRowCount());
        List<Info> infoList = infoMapper.selectByUserName(pageBean.getSearchPhrase());
        PageInfo<Info> pi = new PageInfo<>(infoList);
        return pi;
    }

    @Override
    public int insertInfo(Info info) {
        return infoMapper.insertSelective(info);
    }

    @Override
    public void deleteInfo(int mid) {
        infoMapper.deleteByPrimaryKey(mid);
    }
}
