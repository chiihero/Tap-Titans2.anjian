/*
 Navicat Premium Data Transfer

 Source Server         : tt2
 Source Server Type    : MySQL
 Source Server Version : 50562
 Source Host           : 139.199.11.57:3306
 Source Schema         : tt2anjian

 Target Server Type    : MySQL
 Target Server Version : 50562
 File Encoding         : 65001

 Date: 05/08/2019 23:26:09
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for info
-- ----------------------------
DROP TABLE IF EXISTS `info`;
CREATE TABLE `info`  (
  `mid` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `notes` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `layer_set` int(10) NULL DEFAULT NULL,
  `update_all` int(10) NULL DEFAULT NULL,
  `update_mini` int(10) NULL DEFAULT NULL,
  `time` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `username` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`mid`) USING BTREE,
  INDEX `iuname2uuname`(`username`) USING BTREE,
  CONSTRAINT `iuname2uuname` FOREIGN KEY (`username`) REFERENCES `user` (`username`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE = InnoDB AUTO_INCREMENT = 212 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Compact;

-- ----------------------------
-- Records of info
-- ----------------------------
INSERT INTO `info` VALUES (58, '31999', '测试', 33000, 0, 6, '2019-08-05 01:22:27', 'chii');
INSERT INTO `info` VALUES (59, '31999', '测试', 33000, 1, 1, '2019-08-05 01:22:27', 'chii');
INSERT INTO `info` VALUES (60, '31999', '测试', 33000, 0, 7, '2019-08-05 01:22:27', 'chii');
INSERT INTO `info` VALUES (61, '31999', '测试', 33000, 1, 0, '2019-08-05 01:22:27', 'chii');
INSERT INTO `info` VALUES (62, '31999', '测试', 33000, 0, 6, '2019-08-05 01:22:27', 'chii');
INSERT INTO `info` VALUES (63, '31999', '测试', 33000, 0, 4, '2019-08-05 01:22:28', 'chii');
INSERT INTO `info` VALUES (64, '31999', '测试', 33000, 1, 5, '2019-08-05 01:22:28', 'chii');
INSERT INTO `info` VALUES (65, '31999', '测试', 33000, 0, 6, '2019-08-05 01:22:28', 'chii');
INSERT INTO `info` VALUES (66, '31999', '测试', 33000, 0, 7, '2019-08-05 01:22:23', 'chii');
INSERT INTO `info` VALUES (67, '31999', '测试', 33000, 0, 5, '2019-08-05 01:22:23', 'chii');
INSERT INTO `info` VALUES (68, '31999', '测试', 33000, 0, 8, '2019-08-05 01:22:23', 'chii');
INSERT INTO `info` VALUES (69, '31999', '测试', 34000, 1, 0, '2019-08-05 01:22:23', 'chii');
INSERT INTO `info` VALUES (70, '31999', '测试', 35000, 11, 8, '2019-08-05 01:22:23', 'chii');
INSERT INTO `info` VALUES (71, '31999', '测试', 34400, 1, 6, '2019-08-05 01:22:23', 'chii');
INSERT INTO `info` VALUES (72, '31999', '测试', 34400, 0, 10, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (73, '31999', '测试', 34400, 0, 9, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (74, '31999', '测试', 34400, 0, 9, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (75, '31999', '测试', 34400, 0, 13, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (76, '31999', '测试', 34400, 0, 9, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (77, '31999', '测试', 34400, 1, 11, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (78, '31999', '测试', 34400, 0, 9, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (79, '31999', '测试', 34400, 0, 10, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (80, '31999', '测试', 34400, 0, 11, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (81, '31999', '测试', 34400, 0, 10, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (82, '31999', '测试', 34400, 0, 8, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (83, '31999', '测试', 34400, 0, 11, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (84, '31999', '测试', 34400, 0, 12, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (85, '31999', '测试', 34400, 0, 12, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (86, '31999', '测试', 34400, 1, 6, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (87, '31999', '测试', 34400, 0, 10, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (88, '31999', '测试', 34400, 0, 11, '2019-08-05 01:22:20', 'chii');
INSERT INTO `info` VALUES (89, '31999', '测试', 34400, 0, 10, '2019-08-05 01:22:17', 'chii');
INSERT INTO `info` VALUES (90, '31999', '测试', 34400, 0, 11, '2019-08-05 01:22:17', 'chii');
INSERT INTO `info` VALUES (91, '31999', '测试', 34400, 0, 9, '2019-08-05 01:22:17', 'chii');
INSERT INTO `info` VALUES (92, '31999', '测试', 34400, 0, 10, '2019-08-05 01:22:17', 'chii');
INSERT INTO `info` VALUES (93, '31999', '测试', 34400, 0, 10, '2019-08-05 01:22:17', 'chii');
INSERT INTO `info` VALUES (94, '31999', '测试', 34400, 1, 13, '2019-08-05 01:22:17', 'chii');
INSERT INTO `info` VALUES (95, '31999', '测试', 34400, 0, 10, '2019-08-05 01:22:17', 'chii');
INSERT INTO `info` VALUES (96, '31999', '测试', 34400, 0, 12, '2019-08-05 01:22:17', 'chii');
INSERT INTO `info` VALUES (97, '31999', '测试', 34400, 0, 12, '2019-08-05 01:22:17', 'chii');
INSERT INTO `info` VALUES (98, '31999', '测试', 34400, 0, 9, '2019-08-05 01:22:17', 'chii');
INSERT INTO `info` VALUES (99, '31999', '测试', 34400, 0, 12, '2019-08-05 01:22:17', 'chii');
INSERT INTO `info` VALUES (100, '31999', '测试', 34400, 0, 8, '2019-08-05 01:22:17', 'chii');
INSERT INTO `info` VALUES (101, '31999', '测试', 34400, 0, 9, '2019-08-05 01:22:17', 'chii');
INSERT INTO `info` VALUES (102, '31999', '测试', 34400, 0, 11, '2019-08-05 01:22:17', 'chii');
INSERT INTO `info` VALUES (103, '31999', '测试', 34400, 0, 11, '2019-08-05 01:22:17', 'chii');
INSERT INTO `info` VALUES (104, '31999', '测试', 34400, 1, 13, '2019-08-05 01:22:14', 'chii');
INSERT INTO `info` VALUES (105, '31999', '测试', 34400, 2, 13, '2019-08-05 01:22:14', 'chii');
INSERT INTO `info` VALUES (106, '31999', '测试', 34400, 0, 11, '2019-08-05 01:22:14', 'chii');
INSERT INTO `info` VALUES (107, '31999', '测试', 34400, 0, 10, '2019-08-05 01:22:14', 'chii');
INSERT INTO `info` VALUES (108, '31999', '测试', 34400, 0, 13, '2019-08-05 01:22:14', 'chii');
INSERT INTO `info` VALUES (109, '31999', '测试', 34400, 0, 11, '2019-08-05 01:22:14', 'chii');
INSERT INTO `info` VALUES (110, '31999', '测试', 34400, 0, 11, '2019-08-05 01:22:14', 'chii');
INSERT INTO `info` VALUES (111, '31999', '测试', 34400, 0, 11, '2019-08-05 01:22:14', 'chii');
INSERT INTO `info` VALUES (112, '31999', '测试', 34400, 0, 12, '2019-08-05 01:22:14', 'chii');
INSERT INTO `info` VALUES (113, '31999', '测试', 34400, 0, 11, '2019-08-05 01:22:14', 'chii');
INSERT INTO `info` VALUES (114, '31999', '测试', 34400, 0, 10, '2019-08-05 01:22:14', 'chii');
INSERT INTO `info` VALUES (115, '31999', '测试', 34400, 0, 10, '2019-08-05 01:22:14', 'chii');
INSERT INTO `info` VALUES (116, '31999', '测试', 34400, 0, 11, '2019-08-05 01:22:11', 'chii');
INSERT INTO `info` VALUES (117, '31999', '测试', 34400, 0, 9, '2019-08-05 01:22:11', 'chii');
INSERT INTO `info` VALUES (118, '31999', '测试', 34400, 0, 11, '2019-08-05 01:22:11', 'chii');
INSERT INTO `info` VALUES (119, '31999', '测试', 34400, 0, 10, '2019-08-05 01:22:11', 'chii');
INSERT INTO `info` VALUES (120, '31999', '测试', 34400, 1, 12, '2019-08-05 01:22:11', 'chii');
INSERT INTO `info` VALUES (121, '31999', '测试', 34400, 0, 11, '2019-08-05 01:22:11', 'chii');
INSERT INTO `info` VALUES (122, '31999', '测试', 34400, 0, 9, '2019-08-05 01:22:11', 'chii');
INSERT INTO `info` VALUES (123, '31999', '测试', 34400, 0, 9, '2019-08-05 01:22:11', 'chii');
INSERT INTO `info` VALUES (124, '31999', '测试', 35000, 9, 7, '2019-08-05 01:22:11', 'chii');
INSERT INTO `info` VALUES (125, '31999', '测试', 35000, 8, 14, '2019-08-05 01:22:11', 'chii');
INSERT INTO `info` VALUES (126, '31999', '测试', 35000, 9, 16, '2019-08-05 01:22:11', 'chii');
INSERT INTO `info` VALUES (127, '31999', '测试', 35000, 7, 14, '2019-08-05 01:22:11', 'chii');
INSERT INTO `info` VALUES (128, '31999', '测试', 35000, 7, 16, '2019-08-05 01:22:11', 'chii');
INSERT INTO `info` VALUES (129, '31999', '测试', 35000, 9, 16, '2019-08-05 01:22:11', 'chii');
INSERT INTO `info` VALUES (130, '31999', '测试', 35000, 9, 14, '2019-08-05 01:22:11', 'chii');
INSERT INTO `info` VALUES (131, '31999', '测试', 35000, 8, 18, '2019-08-05 01:22:07', 'chii');
INSERT INTO `info` VALUES (132, '31999', '测试', 35000, 9, 18, '2019-08-05 01:22:07', 'chii');
INSERT INTO `info` VALUES (133, '31999', '测试', 35000, 8, 15, '2019-08-05 01:22:07', 'chii');
INSERT INTO `info` VALUES (134, '31999', '测试', 35000, 8, 16, '2019-08-05 01:22:07', 'chii');
INSERT INTO `info` VALUES (135, '31999', '测试', 35000, 10, 14, '2019-08-05 01:22:07', 'chii');
INSERT INTO `info` VALUES (136, '31999', '测试', 35000, 7, 14, '2019-08-05 01:22:08', 'chii');
INSERT INTO `info` VALUES (137, '31999', '测试', 36000, 11, 17, '2019-08-05 01:22:08', 'chii');
INSERT INTO `info` VALUES (138, '31999', '测试', 36000, 9, 16, '2019-08-05 01:22:08', 'chii');
INSERT INTO `info` VALUES (139, '31999', '测试', 36000, 12, 14, '2019-08-05 01:22:08', 'chii');
INSERT INTO `info` VALUES (140, '31999', '测试', 36000, 18, 17, '2019-08-05 01:22:08', 'chii');
INSERT INTO `info` VALUES (141, '31999', '测试', 36000, 7, 16, '2019-08-05 01:22:08', 'chii');
INSERT INTO `info` VALUES (142, '31999', '测试', 36000, 11, 17, '2019-08-05 01:22:08', 'chii');
INSERT INTO `info` VALUES (143, '31999', '测试', 36000, 16, 16, '2019-08-05 01:22:08', 'chii');
INSERT INTO `info` VALUES (144, '31999', '测试', 35000, 1, 8, '2019-08-05 01:22:08', 'chii');
INSERT INTO `info` VALUES (145, '31999', '测试', 35000, 0, 11, '2019-08-05 01:22:08', 'chii');
INSERT INTO `info` VALUES (146, '31999', '测试', 35000, 0, 11, '2019-08-05 01:22:08', 'chii');
INSERT INTO `info` VALUES (147, '31999', '测试', 35000, 0, 11, '2019-08-05 01:21:53', 'chii');
INSERT INTO `info` VALUES (148, '31999', '测试', 35000, 2, 11, '2019-08-05 01:21:53', 'chii');
INSERT INTO `info` VALUES (149, '31999', '测试', 35000, 1, 13, '2019-08-05 01:21:53', 'chii');
INSERT INTO `info` VALUES (150, '31999', '测试', 35000, 0, 8, '2019-08-05 01:21:53', 'chii');
INSERT INTO `info` VALUES (151, '31999', '测试', 35000, 0, 9, '2019-08-05 01:21:53', 'chii');
INSERT INTO `info` VALUES (152, '31999', '测试', 35000, 0, 12, '2019-08-05 01:21:53', 'chii');
INSERT INTO `info` VALUES (153, '31999', '测试', 37000, 13, 14, '2019-08-05 01:21:53', 'chii');
INSERT INTO `info` VALUES (154, '31999', '测试', 36100, 4, 17, '2019-08-05 01:21:53', 'chii');
INSERT INTO `info` VALUES (155, '31999', '测试', 36100, 8, 14, '2019-08-05 01:21:53', 'chii');
INSERT INTO `info` VALUES (156, '31999', '测试', 36100, 0, 16, '2019-08-05 01:21:33', 'chii');
INSERT INTO `info` VALUES (157, '31999', '测试', 36100, 0, 16, '2019-08-05 01:21:33', 'chii');
INSERT INTO `info` VALUES (158, '31999', '测试', 36100, 0, 14, '2019-08-05 01:21:39', 'chii');
INSERT INTO `info` VALUES (159, '31999', '测试', 36100, 0, 18, '2019-08-05 01:21:39', 'chii');
INSERT INTO `info` VALUES (160, '31999', '测试', 36100, 1, 17, '2019-08-05 01:21:46', 'chii');
INSERT INTO `info` VALUES (161, '31999', '测试', 36100, 2, 16, '2019-08-05 01:21:46', 'chii');
INSERT INTO `info` VALUES (162, '31999', '测试', 36100, 3, 13, '2019-08-05 01:21:46', 'chii');
INSERT INTO `info` VALUES (163, '31999', '测试', 36100, 2, 16, '2019-08-05 01:21:46', 'chii');
INSERT INTO `info` VALUES (164, '31999', '测试', 36100, 0, 13, '2019-08-05 01:21:48', 'chii');
INSERT INTO `info` VALUES (165, '31999', '测试', 36100, 0, 10, '2019-08-05 01:21:48', 'chii');
INSERT INTO `info` VALUES (180, '31999', '测试', 32000, 10, 20, '2019-08-05 01:07:58', 'chii');
INSERT INTO `info` VALUES (181, '31999', '测试', 32000, 10, 20, '2019-08-05 01:07:58', 'chii');
INSERT INTO `info` VALUES (182, '36176', '正常', 36100, 1, 10, '2019-08-05 01:43:51', 'chii');
INSERT INTO `info` VALUES (183, '36122', '正常', 36100, 0, 9, '2019-08-05 02:13:06', 'chii');
INSERT INTO `info` VALUES (184, '36147', '正常', 36100, 0, 9, '2019-08-05 02:44:18', 'chii');
INSERT INTO `info` VALUES (185, '36136', '正常', 36100, 0, 10, '2019-08-05 06:16:17', 'chii');
INSERT INTO `info` VALUES (186, '36128', '正常', 36100, 0, 10, '2019-08-05 06:50:07', 'chii');
INSERT INTO `info` VALUES (187, '36106', '正常', 36100, 0, 11, '2019-08-05 07:23:43', 'chii');
INSERT INTO `info` VALUES (188, '36153', '正常', 36100, 0, 9, '2019-08-05 07:57:29', 'chii');
INSERT INTO `info` VALUES (189, '36102', '正常', 36100, 0, 9, '2019-08-05 08:29:17', 'chii');
INSERT INTO `info` VALUES (190, '36133', '正常', 36100, 0, 9, '2019-08-05 09:04:38', 'chii');
INSERT INTO `info` VALUES (191, '36110', '正常', 36100, 0, 10, '2019-08-05 09:34:17', 'chii');
INSERT INTO `info` VALUES (192, '36135', '正常', 36100, 0, 10, '2019-08-05 10:05:35', 'chii');
INSERT INTO `info` VALUES (193, '36136', '正常', 36100, 0, 9, '2019-08-05 10:37:46', 'chii');
INSERT INTO `info` VALUES (194, '36166', '正常', 36100, 0, 9, '2019-08-05 11:13:13', 'chii');
INSERT INTO `info` VALUES (195, '36193', '正常', 36100, 0, 10, '2019-08-05 11:45:18', 'chii');
INSERT INTO `info` VALUES (196, '36119', '正常', 36100, 1, 12, '2019-08-05 12:17:20', 'chii');
INSERT INTO `info` VALUES (197, '36136', '正常', 36100, 0, 9, '2019-08-05 12:46:57', 'chii');
INSERT INTO `info` VALUES (198, '36125', '正常', 36100, 0, 10, '2019-08-05 13:22:33', 'chii');
INSERT INTO `info` VALUES (199, '36122', '正常', 36100, 0, 8, '2019-08-05 13:53:26', 'chii');
INSERT INTO `info` VALUES (200, '36100', '正常', 36100, 0, 12, '2019-08-05 14:28:40', 'chii');
INSERT INTO `info` VALUES (201, '36183', '异常测试', 36100, 0, 10, '2019-08-05 15:05:43', 'chii');
INSERT INTO `info` VALUES (202, '36105', '正常', 36100, 0, 9, '2019-08-05 15:35:26', 'chii');
INSERT INTO `info` VALUES (203, '36104', '正常', 36100, 0, 10, '2019-08-05 16:10:03', 'chii');
INSERT INTO `info` VALUES (204, '31999', '测试', 32000, 10, 20, '2019-08-05 16:23:23', 'chii1');
INSERT INTO `info` VALUES (205, '31999', '测试', 32000, 10, 20, '2019-08-05 16:23:23', 'chii1');
INSERT INTO `info` VALUES (206, '36106', '正常', 36100, 0, 10, '2019-08-05 16:41:58', 'chii');
INSERT INTO `info` VALUES (207, '36104', '正常', 36100, 0, 9, '2019-08-05 17:16:07', 'chii');
INSERT INTO `info` VALUES (208, '36100', '正常', 36100, 0, 10, '2019-08-05 17:51:42', 'chii');
INSERT INTO `info` VALUES (209, '36112', '正常', 36100, 1, 4, '2019-08-05 21:54:43', 'chii');
INSERT INTO `info` VALUES (210, '36154', '正常', 36100, 0, 10, '2019-08-05 22:26:11', 'chii');
INSERT INTO `info` VALUES (211, '36136', '正常', 36100, 0, 9, '2019-08-05 22:58:42', 'chii');

SET FOREIGN_KEY_CHECKS = 1;
