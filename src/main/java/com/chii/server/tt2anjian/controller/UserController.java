package com.chii.server.tt2anjian.controller;


import com.chii.server.tt2anjian.Tt2anjianApplication;
import com.chii.server.tt2anjian.Utils.SafePasswd;
import com.chii.server.tt2anjian.pojo.User;
import com.chii.server.tt2anjian.service.UserService;
import com.google.gson.Gson;
import org.apache.commons.logging.Log;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.Date;


@RestController
//@RequestMapping("/test")
public class UserController {
    @Autowired
    private UserService userService;

    private static final Logger logger = LoggerFactory.getLogger(Tt2anjianApplication.class);

    @GetMapping("/hello")
    public String hello() {
        logger.info("hello chii" + new Date());
        return "hello chii";
    }

    @GetMapping("/user")
    public User userinfo(@ModelAttribute("username") String username) {
        System.out.println(username);
        User user = userService.getUserInfoByUsername(username);
        if (user != null) {
            logger.info(new Gson().toJson(user));
        }
        return user;
    }

    //密码md5：58ab0aa429cbe3e4a7ece65a4dd5cdfc
    @PostMapping("/signin")
    public User signin(@ModelAttribute("username") String username, @ModelAttribute("passwd") String passwd) {
        User user = userService.getUserInfoByUsername(username);
        if (user != null) {
            passwd = SafePasswd.safe_password(passwd, username, 10);
            if (user.getPasswd().equals(passwd)) {
                logger.info("login success");
                return user;
            }
        }
        return null;
    }

    @PostMapping("/register")
    public String register(@ModelAttribute("username") String username, @ModelAttribute("email") String email, @ModelAttribute("passwd") String passwd) {

        User user = userService.getUserInfoByUsername(username);
        passwd = SafePasswd.safe_password(passwd, username, 10);
        if (user != null) {
            return "false";
        } else {
            User newuser = new User();
            newuser.setUsername(username);
            newuser.setMail(email);
            newuser.setPasswd(passwd);
            userService.insertUserInfo(newuser);
            return "true";
        }
    }
}
