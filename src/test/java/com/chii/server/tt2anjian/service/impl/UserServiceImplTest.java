package com.chii.server.tt2anjian.service.impl;

import com.chii.server.tt2anjian.mapper.UserMapper;
import com.chii.server.tt2anjian.pojo.User;
import com.chii.server.tt2anjian.service.UserService;
import io.lettuce.core.RedisClient;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.cache.annotation.Cacheable;
import org.springframework.data.redis.core.StringRedisTemplate;
import org.springframework.test.context.junit4.SpringRunner;

import static org.junit.Assert.*;
@RunWith(SpringRunner.class)
@SpringBootTest
public class UserServiceImplTest {

    @Autowired
    UserService userService;
    @Test
    @Cacheable(value = "userinfo")
    public void getUserInfoByUsername() {
        User user = userService.getUserInfoByUsername("chii");
        System.out.println(user.getUsername());
        System.out.println(user.getPasswd());
    }


}