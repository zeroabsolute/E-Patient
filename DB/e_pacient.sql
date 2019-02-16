-- phpMyAdmin SQL Dump
-- version 4.8.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 16, 2019 at 07:03 PM
-- Server version: 10.1.37-MariaDB
-- PHP Version: 7.3.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `e_pacient`
--

-- --------------------------------------------------------

--
-- Table structure for table `allergen`
--

CREATE TABLE `allergen` (
  `id` int(11) NOT NULL,
  `patient_chart` int(11) NOT NULL,
  `medicament` int(11) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `allergen`
--

INSERT INTO `allergen` (`id`, `patient_chart`, `medicament`, `status`) VALUES
(1, 1, 1, 0),
(2, 1, 2, 0),
(3, 3, 2, 0),
(6, 3, 1, 0),
(9, 2, 1, 0);

-- --------------------------------------------------------

--
-- Table structure for table `chart_document`
--

CREATE TABLE `chart_document` (
  `id` int(11) NOT NULL,
  `name` text NOT NULL,
  `type` text NOT NULL,
  `url` text NOT NULL,
  `date_created` datetime NOT NULL,
  `patient_chart` int(11) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `chart_document`
--

INSERT INTO `chart_document` (`id`, `name`, `type`, `url`, `date_created`, `patient_chart`, `status`) VALUES
(1, 'Grafi mushkerie', 'Grafi', 'C:\\\\Desktop\\\\Grafi Mushkerish.pdf', '2019-02-03 00:00:00', 1, 0),
(2, 'Analiza 1', 'Analizë', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra - EPacient\\Rezervim 18.pdf', '2019-02-04 00:36:06', 1, 0),
(3, 'Test', 'Tjetër', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra - EPacient\\Kërkesa Funksionale ePacient.pdf', '2019-02-04 00:38:00', 1, 0),
(4, 'Grafi 2', 'Grafi', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra - EPacient\\Rezervim 18.pdf', '2019-02-04 00:40:07', 1, 0),
(5, 'Grafi 1', 'Grafi', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra - EPacient\\Rezervim 18.pdf', '2019-02-04 00:40:36', 2, 0),
(6, 'Grafi zemre', 'Grafi', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra - EPacient\\Kartela.pdf', '2019-02-05 00:31:13', 1, 0),
(7, 'Analiza 5', 'Analizë', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra - EPacient\\Rezervim 18.pdf', '2019-02-05 00:31:31', 1, 0),
(8, 'Doc test 3 ', 'Tjetër', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra - EPacient\\Kartela.pdf', '2019-02-05 00:31:50', 1, 0);

-- --------------------------------------------------------

--
-- Table structure for table `doctor`
--

CREATE TABLE `doctor` (
  `id` int(11) NOT NULL,
  `employee` int(11) NOT NULL,
  `specialized_in` int(11) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `doctor`
--

INSERT INTO `doctor` (`id`, `employee`, `specialized_in`, `status`) VALUES
(13, 13, 1, 0),
(14, 14, 3, 0),
(15, 15, 3, 0),
(16, 16, 6, 0),
(17, 17, 3, 0);

-- --------------------------------------------------------

--
-- Table structure for table `emergency_doctor`
--

CREATE TABLE `emergency_doctor` (
  `id` int(11) NOT NULL,
  `doctor` int(11) NOT NULL,
  `date` date NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `emergency_doctor`
--

INSERT INTO `emergency_doctor` (`id`, `doctor`, `date`, `status`) VALUES
(199, 13, '2019-01-01', 0),
(200, 13, '2019-01-02', 0),
(201, 13, '2019-01-03', 0),
(202, 13, '2019-01-04', 0),
(203, 13, '2019-01-05', 0),
(204, 13, '2019-01-06', 0),
(205, 13, '2019-01-07', 0),
(206, 13, '2019-01-08', 0),
(207, 13, '2019-01-09', 0),
(208, 13, '2019-01-10', 0),
(209, 13, '2019-01-11', 0),
(210, 13, '2019-01-12', 0),
(211, 13, '2019-01-13', 0),
(212, 13, '2019-01-14', 0),
(213, 13, '2019-01-15', 0),
(214, 13, '2019-01-16', 0),
(215, 13, '2019-01-17', 0),
(216, 13, '2019-01-18', 0),
(217, 13, '2019-01-19', 0),
(218, 13, '2019-01-20', 0),
(219, 13, '2019-01-21', 0),
(220, 13, '2019-01-22', 0),
(221, 13, '2019-01-23', 0),
(222, 13, '2019-01-24', 0),
(223, 13, '2019-01-25', 0),
(224, 13, '2019-01-26', 0),
(225, 13, '2019-01-27', 0),
(226, 13, '2019-01-28', 0),
(227, 13, '2019-01-29', 0),
(228, 13, '2019-01-30', 0),
(229, 13, '2019-01-31', 0),
(230, 13, '2019-02-01', 0),
(231, 13, '2019-02-02', 0),
(232, 13, '2019-02-03', 0),
(233, 13, '2019-02-04', 0),
(234, 13, '2019-02-05', 0),
(235, 13, '2019-02-06', 0),
(236, 13, '2019-02-07', 0),
(237, 13, '2019-02-08', 0),
(238, 13, '2019-02-09', 0),
(239, 13, '2019-02-10', 0),
(240, 13, '2019-02-11', 0),
(241, 13, '2019-02-12', 0),
(242, 13, '2019-02-13', 0),
(243, 13, '2019-02-14', 0),
(244, 13, '2019-02-15', 0),
(245, 13, '2019-02-16', 0),
(246, 13, '2019-02-17', 0),
(247, 13, '2019-02-18', 0),
(248, 13, '2019-02-19', 0),
(249, 13, '2019-02-20', 0),
(250, 13, '2019-02-21', 0),
(251, 13, '2019-02-22', 0),
(252, 13, '2019-02-23', 0),
(253, 13, '2019-02-24', 0),
(254, 13, '2019-02-25', 0),
(255, 13, '2019-02-26', 0),
(256, 13, '2019-02-27', 0),
(257, 13, '2019-02-28', 0),
(258, 14, '2019-01-01', 0),
(259, 15, '2019-01-02', 0),
(260, 17, '2019-01-03', 0),
(261, 14, '2019-01-04', 0),
(262, 15, '2019-01-05', 0),
(263, 17, '2019-01-06', 0),
(264, 14, '2019-01-07', 0),
(265, 15, '2019-01-08', 0),
(266, 17, '2019-01-09', 0),
(267, 14, '2019-01-10', 0),
(268, 15, '2019-01-11', 0),
(269, 17, '2019-01-12', 0),
(270, 14, '2019-01-13', 0),
(271, 15, '2019-01-14', 0),
(272, 17, '2019-01-15', 0),
(273, 14, '2019-01-16', 0),
(274, 15, '2019-01-17', 0),
(275, 17, '2019-01-18', 0),
(276, 14, '2019-01-19', 0),
(277, 15, '2019-01-20', 0),
(278, 17, '2019-01-21', 0),
(279, 14, '2019-01-22', 0),
(280, 15, '2019-01-23', 0),
(281, 17, '2019-01-24', 0),
(282, 14, '2019-01-25', 0),
(283, 15, '2019-01-26', 0),
(284, 17, '2019-01-27', 0),
(285, 14, '2019-01-28', 0),
(286, 15, '2019-01-29', 0),
(287, 17, '2019-01-30', 0),
(288, 14, '2019-01-31', 0),
(289, 15, '2019-02-01', 0),
(290, 17, '2019-02-02', 0),
(291, 14, '2019-02-03', 0),
(292, 15, '2019-02-04', 0),
(293, 17, '2019-02-05', 0),
(294, 14, '2019-02-06', 0),
(295, 15, '2019-02-07', 0),
(296, 17, '2019-02-08', 0),
(297, 14, '2019-02-09', 0),
(298, 15, '2019-02-10', 0),
(299, 17, '2019-02-11', 0),
(300, 14, '2019-02-12', 0),
(301, 15, '2019-02-13', 0),
(302, 17, '2019-02-14', 0),
(303, 14, '2019-02-15', 0),
(304, 15, '2019-02-16', 0),
(305, 17, '2019-02-17', 0),
(306, 14, '2019-02-18', 0),
(307, 15, '2019-02-19', 0),
(308, 17, '2019-02-20', 0),
(309, 14, '2019-02-21', 0),
(310, 15, '2019-02-22', 0),
(311, 17, '2019-02-23', 0),
(312, 14, '2019-02-24', 0),
(313, 15, '2019-02-25', 0),
(314, 17, '2019-02-26', 0),
(315, 14, '2019-02-27', 0),
(316, 15, '2019-02-28', 0),
(317, 17, '2019-03-01', 0),
(318, 14, '2019-03-02', 0),
(319, 15, '2019-03-03', 0),
(320, 17, '2019-03-04', 0),
(321, 14, '2019-03-05', 0),
(322, 15, '2019-03-06', 0),
(323, 17, '2019-03-07', 0),
(324, 14, '2019-03-08', 0),
(325, 15, '2019-03-09', 0),
(326, 17, '2019-03-10', 0),
(327, 14, '2019-03-11', 0),
(328, 15, '2019-03-12', 0),
(329, 17, '2019-03-13', 0),
(330, 14, '2019-03-14', 0),
(331, 15, '2019-03-15', 0),
(332, 17, '2019-03-16', 0),
(333, 14, '2019-03-17', 0),
(334, 15, '2019-03-18', 0),
(335, 17, '2019-03-19', 0),
(336, 14, '2019-03-20', 0),
(337, 15, '2019-03-21', 0),
(338, 17, '2019-03-22', 0),
(339, 14, '2019-03-23', 0),
(340, 15, '2019-03-24', 0),
(341, 17, '2019-03-25', 0),
(342, 14, '2019-03-26', 0),
(343, 15, '2019-03-27', 0),
(344, 17, '2019-03-28', 0),
(345, 14, '2019-03-29', 0),
(346, 15, '2019-03-30', 0),
(347, 17, '2019-03-31', 0),
(348, 14, '2019-04-01', 0),
(349, 15, '2019-04-02', 0),
(350, 17, '2019-04-03', 0),
(351, 14, '2019-04-04', 0),
(352, 15, '2019-04-05', 0),
(353, 17, '2019-04-06', 0),
(354, 14, '2019-04-07', 0),
(355, 15, '2019-04-08', 0),
(356, 17, '2019-04-09', 0),
(357, 14, '2019-04-10', 0),
(358, 15, '2019-04-11', 0),
(359, 17, '2019-04-12', 0),
(360, 14, '2019-04-13', 0),
(361, 15, '2019-04-14', 0),
(362, 17, '2019-04-15', 0),
(363, 14, '2019-04-16', 0),
(364, 15, '2019-04-17', 0),
(365, 17, '2019-04-18', 0),
(366, 14, '2019-04-19', 0),
(367, 15, '2019-04-20', 0),
(368, 17, '2019-04-21', 0),
(369, 14, '2019-04-22', 0),
(370, 15, '2019-04-23', 0),
(371, 17, '2019-04-24', 0),
(372, 14, '2019-04-25', 0),
(373, 15, '2019-04-26', 0),
(374, 17, '2019-04-27', 0),
(375, 14, '2019-04-28', 0),
(376, 15, '2019-04-29', 0),
(377, 17, '2019-04-30', 0),
(378, 16, '2019-04-01', 0),
(379, 16, '2019-04-02', 0),
(380, 16, '2019-04-03', 0),
(381, 16, '2019-04-04', 0),
(382, 16, '2019-04-05', 0),
(383, 16, '2019-04-06', 0),
(384, 16, '2019-04-07', 0),
(385, 16, '2019-04-08', 0),
(386, 16, '2019-04-09', 0),
(387, 16, '2019-04-10', 0),
(388, 16, '2019-04-11', 0),
(389, 16, '2019-04-12', 0),
(390, 16, '2019-04-13', 0),
(391, 16, '2019-04-14', 0),
(392, 16, '2019-04-15', 0),
(393, 16, '2019-04-16', 0),
(394, 16, '2019-04-17', 0),
(395, 16, '2019-04-18', 0),
(396, 16, '2019-04-19', 0),
(397, 16, '2019-04-20', 0),
(398, 16, '2019-04-21', 0),
(399, 16, '2019-04-22', 0),
(400, 16, '2019-04-23', 0),
(401, 16, '2019-04-24', 0),
(402, 16, '2019-04-25', 0),
(403, 16, '2019-04-26', 0),
(404, 16, '2019-04-27', 0),
(405, 16, '2019-04-28', 0),
(406, 16, '2019-04-29', 0),
(407, 16, '2019-04-30', 0),
(408, 16, '2019-03-01', 0),
(409, 16, '2019-03-02', 0),
(410, 16, '2019-03-03', 0),
(411, 16, '2019-03-04', 0),
(412, 16, '2019-03-05', 0),
(413, 16, '2019-03-06', 0),
(414, 16, '2019-03-07', 0),
(415, 16, '2019-03-08', 0),
(416, 16, '2019-03-09', 0),
(417, 16, '2019-03-10', 0),
(418, 16, '2019-03-11', 0),
(419, 16, '2019-03-12', 0),
(420, 16, '2019-03-13', 0),
(421, 16, '2019-03-14', 0),
(422, 16, '2019-03-15', 0),
(423, 16, '2019-03-16', 0),
(424, 16, '2019-03-17', 0),
(425, 16, '2019-03-18', 0),
(426, 16, '2019-03-19', 0),
(427, 16, '2019-03-20', 0),
(428, 16, '2019-03-21', 0),
(429, 16, '2019-03-22', 0),
(430, 16, '2019-03-23', 0),
(431, 16, '2019-03-24', 0),
(432, 16, '2019-03-25', 0),
(433, 16, '2019-03-26', 0),
(434, 16, '2019-03-27', 0),
(435, 16, '2019-03-28', 0),
(436, 16, '2019-03-29', 0),
(437, 16, '2019-03-30', 0),
(438, 16, '2019-03-31', 0);

-- --------------------------------------------------------

--
-- Table structure for table `employee`
--

CREATE TABLE `employee` (
  `id` int(11) NOT NULL,
  `first_name` text NOT NULL,
  `last_name` text NOT NULL,
  `phone_number` text NOT NULL,
  `address` text NOT NULL,
  `date_of_birth` date NOT NULL,
  `user` int(11) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `employee`
--

INSERT INTO `employee` (`id`, `first_name`, `last_name`, `phone_number`, `address`, `date_of_birth`, `user`, `status`) VALUES
(2, 'Nurse', '1', '0987654321', 'Rruga e Kavajës, Tiranë, Albania', '1985-01-01', 4, 0),
(3, 'Nurse', '2', '0192837465', 'Rruga e Barrikadave, Tiranë, Albania', '1989-06-06', 14, 0),
(13, 'Doctor', '1', '01234567891', 'Rruga e Elbasanit', '1960-02-08', 25, 0),
(14, 'Doctor', '2', '01234567891', 'Rruga e Elbasanit', '1960-02-08', 26, 0),
(15, 'Doctor', '3', '01234567891', 'Rruga e Elbasanit', '1960-02-08', 27, 0),
(16, 'Doctor', '4', '01234567891', 'Rruga e Elbasanit', '1960-02-08', 28, 0),
(17, 'Doctor', '5', '01234567891', 'Rruga e Barrikadave', '1960-02-08', 29, 0);

-- --------------------------------------------------------

--
-- Table structure for table `manager`
--

CREATE TABLE `manager` (
  `id` int(11) NOT NULL,
  `first_name` text NOT NULL,
  `last_name` text NOT NULL,
  `user` int(11) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `manager`
--

INSERT INTO `manager` (`id`, `first_name`, `last_name`, `user`, `status`) VALUES
(1, 'Manager', '1', 2, 0);

-- --------------------------------------------------------

--
-- Table structure for table `medicament`
--

CREATE TABLE `medicament` (
  `id` int(11) NOT NULL,
  `name` text NOT NULL,
  `description` text NOT NULL,
  `expiration_date` date NOT NULL,
  `ingredients` text NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `medicament`
--

INSERT INTO `medicament` (`id`, `name`, `description`, `expiration_date`, `ingredients`, `status`) VALUES
(1, 'Paracetamol', 'Lehtësues dhimbjesh', '2021-01-23', 'Përbërës 1, Përbërës 2, Përbërës 3, Përbërës 4', 0),
(2, 'Ibuprofen', 'Anti-Inflamator', '2024-01-23', 'Përbërës 1, Përbërës 2, Përbërës 3', 0);

-- --------------------------------------------------------

--
-- Table structure for table `nurse`
--

CREATE TABLE `nurse` (
  `id` int(11) NOT NULL,
  `employee` int(11) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `nurse`
--

INSERT INTO `nurse` (`id`, `employee`, `status`) VALUES
(2, 2, 0),
(3, 3, 0);

-- --------------------------------------------------------

--
-- Table structure for table `operator`
--

CREATE TABLE `operator` (
  `id` int(11) NOT NULL,
  `first_name` text NOT NULL,
  `last_name` text NOT NULL,
  `date_of_birth` date NOT NULL,
  `user` int(11) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `operator`
--

INSERT INTO `operator` (`id`, `first_name`, `last_name`, `date_of_birth`, `user`, `status`) VALUES
(1, 'Operator', '1', '1970-01-01', 1, 0),
(2, 'Operator', '2', '1983-01-01', 12, 0),
(3, 'Operator', '3', '1990-01-21', 13, 0);

-- --------------------------------------------------------

--
-- Table structure for table `patient`
--

CREATE TABLE `patient` (
  `id` int(11) NOT NULL,
  `first_name` text NOT NULL,
  `last_name` text NOT NULL,
  `date_of_birth` date NOT NULL,
  `gender` text NOT NULL,
  `phone_number` text NOT NULL,
  `address` text NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `patient`
--

INSERT INTO `patient` (`id`, `first_name`, `last_name`, `date_of_birth`, `gender`, `phone_number`, `address`, `status`) VALUES
(1, 'Pacient', '1', '1986-01-26', 'Mashkull', '0192837465', 'Rruga e Elbasanit, Tirana, Albania', 0),
(2, 'Pacient', '2', '2000-01-26', 'Femër', '0982746351', 'Komuna e Parisit, Tirana, Albania', 0),
(3, 'Pacient', '3', '1988-02-03', 'Femër', '0192834529', 'Rruga Naim Frashëri', 0);

-- --------------------------------------------------------

--
-- Table structure for table `patient_chart`
--

CREATE TABLE `patient_chart` (
  `id` int(11) NOT NULL,
  `date_created` datetime NOT NULL,
  `patient` int(11) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `patient_chart`
--

INSERT INTO `patient_chart` (`id`, `date_created`, `patient`, `status`) VALUES
(1, '2019-02-28 00:00:00', 1, 0),
(2, '2019-02-10 00:00:00', 2, 0),
(3, '2019-02-03 00:00:00', 3, 0);

-- --------------------------------------------------------

--
-- Table structure for table `receipt`
--

CREATE TABLE `receipt` (
  `id` int(11) NOT NULL,
  `description` text NOT NULL,
  `reservation` int(11) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `receipt_medicament`
--

CREATE TABLE `receipt_medicament` (
  `id` int(11) NOT NULL,
  `recipt` int(11) NOT NULL,
  `medicament` int(11) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `reservation`
--

CREATE TABLE `reservation` (
  `id` int(11) NOT NULL,
  `start_datetime` datetime NOT NULL,
  `end_datetime` datetime NOT NULL,
  `service` int(11) NOT NULL,
  `created_by` int(11) NOT NULL,
  `patient` int(11) NOT NULL,
  `nurse` int(11) NOT NULL,
  `doctor` int(11) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `role`
--

CREATE TABLE `role` (
  `id` int(11) NOT NULL,
  `name` text NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `role`
--

INSERT INTO `role` (`id`, `name`, `status`) VALUES
(1, 'Operator', 0),
(2, 'Manager', 0),
(3, 'Nurse', 0),
(4, 'Doctor', 0);

-- --------------------------------------------------------

--
-- Table structure for table `sector`
--

CREATE TABLE `sector` (
  `id` int(11) NOT NULL,
  `name` text NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `sector`
--

INSERT INTO `sector` (`id`, `name`, `status`) VALUES
(1, 'Kardiologji', 0),
(2, 'Anatomi', 0),
(3, 'Neurologji', 0),
(4, 'Fiziologji', 0),
(5, 'Imunologji', 0),
(6, 'Hematologji', 0);

-- --------------------------------------------------------

--
-- Table structure for table `service`
--

CREATE TABLE `service` (
  `id` int(11) NOT NULL,
  `name` text NOT NULL,
  `fee` int(11) NOT NULL,
  `description` text NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `service`
--

INSERT INTO `service` (`id`, `name`, `fee`, `description`, `status`) VALUES
(1, 'Analizë e plotë e zemrës', 8500, 'EKG, ushtrime, matje pulsi ...', 0),
(2, 'Analiza të mushkërive', 3500, 'Konsultë me specialistin, grafi', 0);

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `email` varchar(128) NOT NULL,
  `password` text NOT NULL,
  `role` int(11) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `email`, `password`, `role`, `status`) VALUES
(1, 'operator1@epacient.al', 'lOK3eARkD6HpVqWrR/XwqkaIPRZN481FQXias4ioJrEPR6bl', 1, 0),
(2, 'manager1@epacient.al', 'hLRx8jFeExamFEOx+9VttCMJDbnugy6HuWGX2Zaa/ae48Kdo', 2, 0),
(4, 'nurse1@epacient.al', 'hDwwlUS12iBOHb5U+hWottB+QmFJPL9cTdTebhbozKtfClhW', 3, 0),
(12, 'operator2@epacient.al', 'WWK7/AaPdOBibFnDdtDqLy+CqiVFWlbuetT/pd8M4EnkPf6R', 1, 0),
(13, 'operator3@epacient.al', '6fFx+jo6ay12hK1v5YiRrUY1TDx6dojiKHfpZuKeQXa9NXQE', 1, 0),
(14, 'nurse2@epacient.al', '1qMCIFNnQRXgaZUJZpFFonU4ufYYJsMzxco6r3NKvV0rZsyB', 3, 0),
(25, 'doctor1@epacient.al', 'Z5A7BZ0971plGwbCqe4GUk4MUQm5iPZbHsq8ZZ9O9OYe5UvV', 4, 0),
(26, 'doctor2@epacient.al', 'nBHV83XgTd8nTWryCiKtHwzJNtu2TAGTH6JZ3160/jxFaKyz', 4, 0),
(27, 'doctor3@epacient.al', 'ey3cnZBdMfZsCKB1zIWHo4vXBhakx1QVmdbZ/55amx/cWNyj', 4, 0),
(28, 'doctor4@epacient.al', 'SmkmHpLwCx2FoX+vDTqZmc7XQ8rYTw1/tpq9FzVmT2BftWKg', 4, 0),
(29, 'doctor5@epacient.al', 'uh92sNH2kudGfr0RAc8x9v4lJH6LMKOuANDqrnFWLjdT29TS', 4, 0);

-- --------------------------------------------------------

--
-- Table structure for table `working_hours`
--

CREATE TABLE `working_hours` (
  `id` int(11) NOT NULL,
  `monday_start_time` datetime NOT NULL,
  `monday_end_time` datetime NOT NULL,
  `tuesday_start_time` datetime NOT NULL,
  `tuesday_end_time` datetime NOT NULL,
  `wednesday_start_time` datetime NOT NULL,
  `wednesday_end_time` datetime NOT NULL,
  `thursday_start_time` datetime NOT NULL,
  `thursday_end_time` datetime NOT NULL,
  `friday_start_time` datetime NOT NULL,
  `friday_end_time` datetime NOT NULL,
  `saturday_start_time` datetime NOT NULL,
  `saturday_end_time` datetime NOT NULL,
  `sunday_start_time` datetime NOT NULL,
  `sunday_end_time` datetime NOT NULL,
  `employee` int(11) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `working_hours`
--

INSERT INTO `working_hours` (`id`, `monday_start_time`, `monday_end_time`, `tuesday_start_time`, `tuesday_end_time`, `wednesday_start_time`, `wednesday_end_time`, `thursday_start_time`, `thursday_end_time`, `friday_start_time`, `friday_end_time`, `saturday_start_time`, `saturday_end_time`, `sunday_start_time`, `sunday_end_time`, `employee`, `status`) VALUES
(5, '2019-01-23 02:00:00', '2019-01-23 10:00:00', '2019-01-23 20:00:00', '2019-01-23 04:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', 3, 0),
(6, '2019-02-01 08:00:00', '2019-02-01 16:00:00', '2019-02-01 08:00:00', '2019-02-01 16:00:00', '2019-02-01 08:00:00', '2019-02-01 16:00:00', '2019-02-01 08:00:00', '2019-02-01 16:00:00', '2019-02-01 08:00:00', '2019-02-01 16:00:00', '2019-02-01 08:00:00', '2019-02-01 16:00:00', '2019-02-01 08:00:00', '2019-02-01 16:00:00', 2, 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `allergen`
--
ALTER TABLE `allergen`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `medicament` (`medicament`,`patient_chart`),
  ADD KEY `patient_chart` (`patient_chart`);

--
-- Indexes for table `chart_document`
--
ALTER TABLE `chart_document`
  ADD PRIMARY KEY (`id`),
  ADD KEY `patient_chart` (`patient_chart`);

--
-- Indexes for table `doctor`
--
ALTER TABLE `doctor`
  ADD PRIMARY KEY (`id`),
  ADD KEY `employee` (`employee`),
  ADD KEY `doctor_ibfk_2` (`specialized_in`);

--
-- Indexes for table `emergency_doctor`
--
ALTER TABLE `emergency_doctor`
  ADD PRIMARY KEY (`id`),
  ADD KEY `doctor` (`doctor`);

--
-- Indexes for table `employee`
--
ALTER TABLE `employee`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user` (`user`);

--
-- Indexes for table `manager`
--
ALTER TABLE `manager`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user` (`user`);

--
-- Indexes for table `medicament`
--
ALTER TABLE `medicament`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `nurse`
--
ALTER TABLE `nurse`
  ADD PRIMARY KEY (`id`),
  ADD KEY `employee` (`employee`);

--
-- Indexes for table `operator`
--
ALTER TABLE `operator`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user` (`user`);

--
-- Indexes for table `patient`
--
ALTER TABLE `patient`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `patient_chart`
--
ALTER TABLE `patient_chart`
  ADD PRIMARY KEY (`id`),
  ADD KEY `patient` (`patient`);

--
-- Indexes for table `receipt`
--
ALTER TABLE `receipt`
  ADD PRIMARY KEY (`id`),
  ADD KEY `reservation` (`reservation`);

--
-- Indexes for table `receipt_medicament`
--
ALTER TABLE `receipt_medicament`
  ADD PRIMARY KEY (`id`),
  ADD KEY `medicament` (`medicament`),
  ADD KEY `recipt` (`recipt`);

--
-- Indexes for table `reservation`
--
ALTER TABLE `reservation`
  ADD PRIMARY KEY (`id`),
  ADD KEY `created_by` (`created_by`),
  ADD KEY `patient` (`patient`),
  ADD KEY `nurse` (`nurse`),
  ADD KEY `service` (`service`),
  ADD KEY `reservation_ibfk_5` (`doctor`);

--
-- Indexes for table `role`
--
ALTER TABLE `role`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `sector`
--
ALTER TABLE `sector`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `service`
--
ALTER TABLE `service`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD KEY `role` (`role`);

--
-- Indexes for table `working_hours`
--
ALTER TABLE `working_hours`
  ADD PRIMARY KEY (`id`),
  ADD KEY `employee` (`employee`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `allergen`
--
ALTER TABLE `allergen`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `chart_document`
--
ALTER TABLE `chart_document`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `doctor`
--
ALTER TABLE `doctor`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `emergency_doctor`
--
ALTER TABLE `emergency_doctor`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=439;

--
-- AUTO_INCREMENT for table `employee`
--
ALTER TABLE `employee`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `manager`
--
ALTER TABLE `manager`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `medicament`
--
ALTER TABLE `medicament`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `nurse`
--
ALTER TABLE `nurse`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `operator`
--
ALTER TABLE `operator`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `patient`
--
ALTER TABLE `patient`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `patient_chart`
--
ALTER TABLE `patient_chart`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `receipt`
--
ALTER TABLE `receipt`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `receipt_medicament`
--
ALTER TABLE `receipt_medicament`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `reservation`
--
ALTER TABLE `reservation`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `role`
--
ALTER TABLE `role`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `sector`
--
ALTER TABLE `sector`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `service`
--
ALTER TABLE `service`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT for table `working_hours`
--
ALTER TABLE `working_hours`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `allergen`
--
ALTER TABLE `allergen`
  ADD CONSTRAINT `allergen_ibfk_1` FOREIGN KEY (`medicament`) REFERENCES `medicament` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `allergen_ibfk_2` FOREIGN KEY (`patient_chart`) REFERENCES `patient_chart` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `chart_document`
--
ALTER TABLE `chart_document`
  ADD CONSTRAINT `chart_document_ibfk_1` FOREIGN KEY (`patient_chart`) REFERENCES `patient_chart` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `doctor`
--
ALTER TABLE `doctor`
  ADD CONSTRAINT `doctor_ibfk_1` FOREIGN KEY (`employee`) REFERENCES `employee` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `doctor_ibfk_2` FOREIGN KEY (`specialized_in`) REFERENCES `sector` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `emergency_doctor`
--
ALTER TABLE `emergency_doctor`
  ADD CONSTRAINT `emergency_doctor_ibfk_1` FOREIGN KEY (`doctor`) REFERENCES `doctor` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `employee`
--
ALTER TABLE `employee`
  ADD CONSTRAINT `employee_ibfk_1` FOREIGN KEY (`user`) REFERENCES `user` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `manager`
--
ALTER TABLE `manager`
  ADD CONSTRAINT `user` FOREIGN KEY (`user`) REFERENCES `user` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `nurse`
--
ALTER TABLE `nurse`
  ADD CONSTRAINT `nurse_ibfk_1` FOREIGN KEY (`employee`) REFERENCES `employee` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `operator`
--
ALTER TABLE `operator`
  ADD CONSTRAINT `operator_ibfk_1` FOREIGN KEY (`user`) REFERENCES `user` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `patient_chart`
--
ALTER TABLE `patient_chart`
  ADD CONSTRAINT `patient_chart_ibfk_1` FOREIGN KEY (`patient`) REFERENCES `patient` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `receipt`
--
ALTER TABLE `receipt`
  ADD CONSTRAINT `receipt_ibfk_1` FOREIGN KEY (`reservation`) REFERENCES `reservation` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `receipt_medicament`
--
ALTER TABLE `receipt_medicament`
  ADD CONSTRAINT `receipt_medicament_ibfk_1` FOREIGN KEY (`medicament`) REFERENCES `medicament` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `receipt_medicament_ibfk_2` FOREIGN KEY (`recipt`) REFERENCES `receipt` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `reservation`
--
ALTER TABLE `reservation`
  ADD CONSTRAINT `reservation_ibfk_1` FOREIGN KEY (`created_by`) REFERENCES `operator` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `reservation_ibfk_3` FOREIGN KEY (`patient`) REFERENCES `patient` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `reservation_ibfk_4` FOREIGN KEY (`nurse`) REFERENCES `nurse` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `reservation_ibfk_5` FOREIGN KEY (`doctor`) REFERENCES `doctor` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `reservation_ibfk_6` FOREIGN KEY (`service`) REFERENCES `service` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `user`
--
ALTER TABLE `user`
  ADD CONSTRAINT `role` FOREIGN KEY (`role`) REFERENCES `role` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `working_hours`
--
ALTER TABLE `working_hours`
  ADD CONSTRAINT `working_hours_ibfk_2` FOREIGN KEY (`employee`) REFERENCES `employee` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
