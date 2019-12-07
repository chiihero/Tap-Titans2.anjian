package com.chii.server.tt2anjian.service.impl;

import com.chii.server.tt2anjian.mapper.UserMapper;
import com.chii.server.tt2anjian.pojo.User;
import com.chii.server.tt2anjian.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.cache.annotation.CacheConfig;
import org.springframework.cache.annotation.CachePut;
import org.springframework.cache.annotation.Cacheable;
import org.springframework.stereotype.Service;

@Service
public class UserServiceImpl implements UserService {
    private final UserMapper userMapper;

    @Autowired
    public UserServiceImpl(UserMapper userMapper) {
        this.userMapper = userMapper;
    }

    @Override
    @Cacheable(value = "redisDemo",key = "#username")
    public User getUserInfoByUsername(String username) {
        return  userMapper.selectByPrimaryKey(username);
    }

    @Override
    public void insertUserInfo(User user) {
        userMapper.insert(user);
    }

    @Override
    public void updateUserPasswd(User user) {
        userMapper.updateByPrimaryKeySelective(user);
    }

    @Override
    public void deleteUserInfo(User user) {
        userMapper.deleteByPrimaryKey(user.getUsername());
    }
}
