package com.chii.server.tt2anjian.service.impl;

import com.chii.server.tt2anjian.mapper.InfoMapper;
import com.chii.server.tt2anjian.mapper.UserMapper;
import com.chii.server.tt2anjian.pojo.Info;
import com.chii.server.tt2anjian.pojo.User;
import com.chii.server.tt2anjian.service.InfoService;
import com.chii.server.tt2anjian.service.UserService;
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
    public List<Info> getInfoInfoByUsername(String username) {
        return infoMapper.selectByUserName(username);
    }

    @Override
    public void insertInfo(Info info) {
        infoMapper.insert(info);
    }

    @Override
    public void deleteInfo(Info info) {
        infoMapper.deleteByPrimaryKey(info.getMid());
    }
}
