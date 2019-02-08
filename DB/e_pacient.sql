-- phpMyAdmin SQL Dump
-- version 4.8.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 08, 2019 at 01:38 AM
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
  `medicament` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `allergen`
--

INSERT INTO `allergen` (`id`, `patient_chart`, `medicament`) VALUES
(1, 1, 1),
(9, 2, 1),
(6, 3, 1),
(2, 1, 2),
(3, 3, 2);

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
  `patient_chart` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `chart_document`
--

INSERT INTO `chart_document` (`id`, `name`, `type`, `url`, `date_created`, `patient_chart`) VALUES
(1, 'Grafi mushkerie', 'Grafi', 'C:\\\\Desktop\\\\Grafi Mushkerish.pdf', '2019-02-03 00:00:00', 1),
(2, 'Analiza 1', 'Analizë', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra - EPacient\\Rezervim 18.pdf', '2019-02-04 00:36:06', 1),
(3, 'Test', 'Tjetër', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra - EPacient\\Kërkesa Funksionale ePacient.pdf', '2019-02-04 00:38:00', 1),
(4, 'Grafi 2', 'Grafi', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra - EPacient\\Rezervim 18.pdf', '2019-02-04 00:40:07', 1),
(5, 'Grafi 1', 'Grafi', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra - EPacient\\Rezervim 18.pdf', '2019-02-04 00:40:36', 2),
(6, 'Grafi zemre', 'Grafi', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra - EPacient\\Kartela.pdf', '2019-02-05 00:31:13', 1),
(7, 'Analiza 5', 'Analizë', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra - EPacient\\Rezervim 18.pdf', '2019-02-05 00:31:31', 1),
(8, 'Doc test 3 ', 'Tjetër', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra - EPacient\\Kartela.pdf', '2019-02-05 00:31:50', 1);

-- --------------------------------------------------------

--
-- Table structure for table `doctor`
--

CREATE TABLE `doctor` (
  `id` int(11) NOT NULL,
  `employee` int(11) NOT NULL,
  `specialized_in` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `doctor`
--

INSERT INTO `doctor` (`id`, `employee`, `specialized_in`) VALUES
(13, 13, 1),
(14, 14, 3),
(15, 15, 3),
(16, 16, 6),
(17, 17, 3);

-- --------------------------------------------------------

--
-- Table structure for table `emergency_doctor`
--

CREATE TABLE `emergency_doctor` (
  `id` int(11) NOT NULL,
  `doctor` int(11) NOT NULL,
  `date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `emergency_doctor`
--

INSERT INTO `emergency_doctor` (`id`, `doctor`, `date`) VALUES
(199, 13, '2019-01-01'),
(200, 13, '2019-01-02'),
(201, 13, '2019-01-03'),
(202, 13, '2019-01-04'),
(203, 13, '2019-01-05'),
(204, 13, '2019-01-06'),
(205, 13, '2019-01-07'),
(206, 13, '2019-01-08'),
(207, 13, '2019-01-09'),
(208, 13, '2019-01-10'),
(209, 13, '2019-01-11'),
(210, 13, '2019-01-12'),
(211, 13, '2019-01-13'),
(212, 13, '2019-01-14'),
(213, 13, '2019-01-15'),
(214, 13, '2019-01-16'),
(215, 13, '2019-01-17'),
(216, 13, '2019-01-18'),
(217, 13, '2019-01-19'),
(218, 13, '2019-01-20'),
(219, 13, '2019-01-21'),
(220, 13, '2019-01-22'),
(221, 13, '2019-01-23'),
(222, 13, '2019-01-24'),
(223, 13, '2019-01-25'),
(224, 13, '2019-01-26'),
(225, 13, '2019-01-27'),
(226, 13, '2019-01-28'),
(227, 13, '2019-01-29'),
(228, 13, '2019-01-30'),
(229, 13, '2019-01-31'),
(230, 13, '2019-02-01'),
(231, 13, '2019-02-02'),
(232, 13, '2019-02-03'),
(233, 13, '2019-02-04'),
(234, 13, '2019-02-05'),
(235, 13, '2019-02-06'),
(236, 13, '2019-02-07'),
(237, 13, '2019-02-08'),
(238, 13, '2019-02-09'),
(239, 13, '2019-02-10'),
(240, 13, '2019-02-11'),
(241, 13, '2019-02-12'),
(242, 13, '2019-02-13'),
(243, 13, '2019-02-14'),
(244, 13, '2019-02-15'),
(245, 13, '2019-02-16'),
(246, 13, '2019-02-17'),
(247, 13, '2019-02-18'),
(248, 13, '2019-02-19'),
(249, 13, '2019-02-20'),
(250, 13, '2019-02-21'),
(251, 13, '2019-02-22'),
(252, 13, '2019-02-23'),
(253, 13, '2019-02-24'),
(254, 13, '2019-02-25'),
(255, 13, '2019-02-26'),
(256, 13, '2019-02-27'),
(257, 13, '2019-02-28'),
(258, 14, '2019-01-01'),
(259, 15, '2019-01-02'),
(260, 17, '2019-01-03'),
(261, 14, '2019-01-04'),
(262, 15, '2019-01-05'),
(263, 17, '2019-01-06'),
(264, 14, '2019-01-07'),
(265, 15, '2019-01-08'),
(266, 17, '2019-01-09'),
(267, 14, '2019-01-10'),
(268, 15, '2019-01-11'),
(269, 17, '2019-01-12'),
(270, 14, '2019-01-13'),
(271, 15, '2019-01-14'),
(272, 17, '2019-01-15'),
(273, 14, '2019-01-16'),
(274, 15, '2019-01-17'),
(275, 17, '2019-01-18'),
(276, 14, '2019-01-19'),
(277, 15, '2019-01-20'),
(278, 17, '2019-01-21'),
(279, 14, '2019-01-22'),
(280, 15, '2019-01-23'),
(281, 17, '2019-01-24'),
(282, 14, '2019-01-25'),
(283, 15, '2019-01-26'),
(284, 17, '2019-01-27'),
(285, 14, '2019-01-28'),
(286, 15, '2019-01-29'),
(287, 17, '2019-01-30'),
(288, 14, '2019-01-31'),
(289, 15, '2019-02-01'),
(290, 17, '2019-02-02'),
(291, 14, '2019-02-03'),
(292, 15, '2019-02-04'),
(293, 17, '2019-02-05'),
(294, 14, '2019-02-06'),
(295, 15, '2019-02-07'),
(296, 17, '2019-02-08'),
(297, 14, '2019-02-09'),
(298, 15, '2019-02-10'),
(299, 17, '2019-02-11'),
(300, 14, '2019-02-12'),
(301, 15, '2019-02-13'),
(302, 17, '2019-02-14'),
(303, 14, '2019-02-15'),
(304, 15, '2019-02-16'),
(305, 17, '2019-02-17'),
(306, 14, '2019-02-18'),
(307, 15, '2019-02-19'),
(308, 17, '2019-02-20'),
(309, 14, '2019-02-21'),
(310, 15, '2019-02-22'),
(311, 17, '2019-02-23'),
(312, 14, '2019-02-24'),
(313, 15, '2019-02-25'),
(314, 17, '2019-02-26'),
(315, 14, '2019-02-27'),
(316, 15, '2019-02-28'),
(317, 17, '2019-03-01'),
(318, 14, '2019-03-02'),
(319, 15, '2019-03-03'),
(320, 17, '2019-03-04'),
(321, 14, '2019-03-05'),
(322, 15, '2019-03-06'),
(323, 17, '2019-03-07'),
(324, 14, '2019-03-08'),
(325, 15, '2019-03-09'),
(326, 17, '2019-03-10'),
(327, 14, '2019-03-11'),
(328, 15, '2019-03-12'),
(329, 17, '2019-03-13'),
(330, 14, '2019-03-14'),
(331, 15, '2019-03-15'),
(332, 17, '2019-03-16'),
(333, 14, '2019-03-17'),
(334, 15, '2019-03-18'),
(335, 17, '2019-03-19'),
(336, 14, '2019-03-20'),
(337, 15, '2019-03-21'),
(338, 17, '2019-03-22'),
(339, 14, '2019-03-23'),
(340, 15, '2019-03-24'),
(341, 17, '2019-03-25'),
(342, 14, '2019-03-26'),
(343, 15, '2019-03-27'),
(344, 17, '2019-03-28'),
(345, 14, '2019-03-29'),
(346, 15, '2019-03-30'),
(347, 17, '2019-03-31'),
(348, 14, '2019-04-01'),
(349, 15, '2019-04-02'),
(350, 17, '2019-04-03'),
(351, 14, '2019-04-04'),
(352, 15, '2019-04-05'),
(353, 17, '2019-04-06'),
(354, 14, '2019-04-07'),
(355, 15, '2019-04-08'),
(356, 17, '2019-04-09'),
(357, 14, '2019-04-10'),
(358, 15, '2019-04-11'),
(359, 17, '2019-04-12'),
(360, 14, '2019-04-13'),
(361, 15, '2019-04-14'),
(362, 17, '2019-04-15'),
(363, 14, '2019-04-16'),
(364, 15, '2019-04-17'),
(365, 17, '2019-04-18'),
(366, 14, '2019-04-19'),
(367, 15, '2019-04-20'),
(368, 17, '2019-04-21'),
(369, 14, '2019-04-22'),
(370, 15, '2019-04-23'),
(371, 17, '2019-04-24'),
(372, 14, '2019-04-25'),
(373, 15, '2019-04-26'),
(374, 17, '2019-04-27'),
(375, 14, '2019-04-28'),
(376, 15, '2019-04-29'),
(377, 17, '2019-04-30'),
(378, 16, '2019-04-01'),
(379, 16, '2019-04-02'),
(380, 16, '2019-04-03'),
(381, 16, '2019-04-04'),
(382, 16, '2019-04-05'),
(383, 16, '2019-04-06'),
(384, 16, '2019-04-07'),
(385, 16, '2019-04-08'),
(386, 16, '2019-04-09'),
(387, 16, '2019-04-10'),
(388, 16, '2019-04-11'),
(389, 16, '2019-04-12'),
(390, 16, '2019-04-13'),
(391, 16, '2019-04-14'),
(392, 16, '2019-04-15'),
(393, 16, '2019-04-16'),
(394, 16, '2019-04-17'),
(395, 16, '2019-04-18'),
(396, 16, '2019-04-19'),
(397, 16, '2019-04-20'),
(398, 16, '2019-04-21'),
(399, 16, '2019-04-22'),
(400, 16, '2019-04-23'),
(401, 16, '2019-04-24'),
(402, 16, '2019-04-25'),
(403, 16, '2019-04-26'),
(404, 16, '2019-04-27'),
(405, 16, '2019-04-28'),
(406, 16, '2019-04-29'),
(407, 16, '2019-04-30'),
(408, 16, '2019-03-01'),
(409, 16, '2019-03-02'),
(410, 16, '2019-03-03'),
(411, 16, '2019-03-04'),
(412, 16, '2019-03-05'),
(413, 16, '2019-03-06'),
(414, 16, '2019-03-07'),
(415, 16, '2019-03-08'),
(416, 16, '2019-03-09'),
(417, 16, '2019-03-10'),
(418, 16, '2019-03-11'),
(419, 16, '2019-03-12'),
(420, 16, '2019-03-13'),
(421, 16, '2019-03-14'),
(422, 16, '2019-03-15'),
(423, 16, '2019-03-16'),
(424, 16, '2019-03-17'),
(425, 16, '2019-03-18'),
(426, 16, '2019-03-19'),
(427, 16, '2019-03-20'),
(428, 16, '2019-03-21'),
(429, 16, '2019-03-22'),
(430, 16, '2019-03-23'),
(431, 16, '2019-03-24'),
(432, 16, '2019-03-25'),
(433, 16, '2019-03-26'),
(434, 16, '2019-03-27'),
(435, 16, '2019-03-28'),
(436, 16, '2019-03-29'),
(437, 16, '2019-03-30'),
(438, 16, '2019-03-31');

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
  `user` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `employee`
--

INSERT INTO `employee` (`id`, `first_name`, `last_name`, `phone_number`, `address`, `date_of_birth`, `user`) VALUES
(2, 'Nurse', '1', '0987654321', 'Rruga e Kavajës, Tiranë, Albania', '1985-01-01', 4),
(3, 'Nurse', '2', '0192837465', 'Rruga e Barrikadave, Tiranë, Albania', '1989-06-06', 14),
(13, 'Doctor', '1', '01234567891', 'Rruga e Elbasanit', '1960-02-08', 25),
(14, 'Doctor', '2', '01234567891', 'Rruga e Elbasanit', '1960-02-08', 26),
(15, 'Doctor', '3', '01234567891', 'Rruga e Elbasanit', '1960-02-08', 27),
(16, 'Doctor', '4', '01234567891', 'Rruga e Elbasanit', '1960-02-08', 28),
(17, 'Doctor', '5', '01234567891', 'Rruga e Barrikadave', '1960-02-08', 29);

-- --------------------------------------------------------

--
-- Table structure for table `manager`
--

CREATE TABLE `manager` (
  `id` int(11) NOT NULL,
  `first_name` text NOT NULL,
  `last_name` text NOT NULL,
  `user` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `manager`
--

INSERT INTO `manager` (`id`, `first_name`, `last_name`, `user`) VALUES
(1, 'Manager', '1', 2);

-- --------------------------------------------------------

--
-- Table structure for table `medicament`
--

CREATE TABLE `medicament` (
  `id` int(11) NOT NULL,
  `name` text NOT NULL,
  `description` text NOT NULL,
  `expiration_date` date NOT NULL,
  `ingredients` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `medicament`
--

INSERT INTO `medicament` (`id`, `name`, `description`, `expiration_date`, `ingredients`) VALUES
(1, 'Paracetamol', 'Lehtësues dhimbjesh', '2021-01-23', 'Përbërës 1, Përbërës 2, Përbërës 3, Përbërës 4'),
(2, 'Ibuprofen', 'Anti-Inflamator', '2024-01-23', 'Përbërës 1, Përbërës 2, Përbërës 3');

-- --------------------------------------------------------

--
-- Table structure for table `nurse`
--

CREATE TABLE `nurse` (
  `id` int(11) NOT NULL,
  `employee` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `nurse`
--

INSERT INTO `nurse` (`id`, `employee`) VALUES
(2, 2),
(3, 3);

-- --------------------------------------------------------

--
-- Table structure for table `operator`
--

CREATE TABLE `operator` (
  `id` int(11) NOT NULL,
  `first_name` text NOT NULL,
  `last_name` text NOT NULL,
  `date_of_birth` date NOT NULL,
  `user` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `operator`
--

INSERT INTO `operator` (`id`, `first_name`, `last_name`, `date_of_birth`, `user`) VALUES
(1, 'Operator', '1', '1970-01-01', 1),
(2, 'Operator', '2', '1983-01-01', 12),
(3, 'Operator', '3', '1990-01-21', 13);

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
  `address` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `patient`
--

INSERT INTO `patient` (`id`, `first_name`, `last_name`, `date_of_birth`, `gender`, `phone_number`, `address`) VALUES
(1, 'Pacient', '1', '1986-01-26', 'Mashkull', '0192837465', 'Rruga e Elbasanit, Tirana, Albania'),
(2, 'Pacient', '2', '2000-01-26', 'Femër', '0982746351', 'Komuna e Parisit, Tirana, Albania'),
(3, 'Pacient', '3', '1988-02-03', 'Femër', '0192834529', 'Rruga Naim Frashëri');

-- --------------------------------------------------------

--
-- Table structure for table `patient_chart`
--

CREATE TABLE `patient_chart` (
  `id` int(11) NOT NULL,
  `date_created` datetime NOT NULL,
  `patient` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `patient_chart`
--

INSERT INTO `patient_chart` (`id`, `date_created`, `patient`) VALUES
(1, '2019-02-28 00:00:00', 1),
(2, '2019-02-10 00:00:00', 2),
(3, '2019-02-03 00:00:00', 3);

-- --------------------------------------------------------

--
-- Table structure for table `receipt`
--

CREATE TABLE `receipt` (
  `id` int(11) NOT NULL,
  `description` text NOT NULL,
  `reservation` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `receipt_medicament`
--

CREATE TABLE `receipt_medicament` (
  `id` int(11) NOT NULL,
  `recipt` int(11) NOT NULL,
  `medicament` int(11) NOT NULL
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
  `doctor` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `role`
--

CREATE TABLE `role` (
  `id` int(11) NOT NULL,
  `name` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `role`
--

INSERT INTO `role` (`id`, `name`) VALUES
(1, 'Operator'),
(2, 'Manager'),
(3, 'Nurse'),
(4, 'Doctor');

-- --------------------------------------------------------

--
-- Table structure for table `sector`
--

CREATE TABLE `sector` (
  `id` int(11) NOT NULL,
  `name` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `sector`
--

INSERT INTO `sector` (`id`, `name`) VALUES
(1, 'Kardiologji'),
(2, 'Anatomi'),
(3, 'Neurologji'),
(4, 'Fiziologji'),
(5, 'Imunologji'),
(6, 'Hematologji');

-- --------------------------------------------------------

--
-- Table structure for table `service`
--

CREATE TABLE `service` (
  `id` int(11) NOT NULL,
  `name` text NOT NULL,
  `fee` int(11) NOT NULL,
  `description` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `service`
--

INSERT INTO `service` (`id`, `name`, `fee`, `description`) VALUES
(1, 'Analizë e plotë e zemrës', 8500, 'EKG, ushtrime, matje pulsi ...'),
(2, 'Analiza të mushkërive', 3500, 'Konsultë me specialistin, grafi');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id` int(11) NOT NULL,
  `email` varchar(128) NOT NULL,
  `password` text NOT NULL,
  `role` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id`, `email`, `password`, `role`) VALUES
(1, 'operator1@epacient.al', 'lOK3eARkD6HpVqWrR/XwqkaIPRZN481FQXias4ioJrEPR6bl', 1),
(2, 'manager1@epacient.al', 'hLRx8jFeExamFEOx+9VttCMJDbnugy6HuWGX2Zaa/ae48Kdo', 2),
(4, 'nurse1@epacient.al', 'hDwwlUS12iBOHb5U+hWottB+QmFJPL9cTdTebhbozKtfClhW', 3),
(12, 'operator2@epacient.al', 'WWK7/AaPdOBibFnDdtDqLy+CqiVFWlbuetT/pd8M4EnkPf6R', 1),
(13, 'operator3@epacient.al', '6fFx+jo6ay12hK1v5YiRrUY1TDx6dojiKHfpZuKeQXa9NXQE', 1),
(14, 'nurse2@epacient.al', '1qMCIFNnQRXgaZUJZpFFonU4ufYYJsMzxco6r3NKvV0rZsyB', 3),
(25, 'doctor1@epacient.al', 'Z5A7BZ0971plGwbCqe4GUk4MUQm5iPZbHsq8ZZ9O9OYe5UvV', 4),
(26, 'doctor2@epacient.al', 'nBHV83XgTd8nTWryCiKtHwzJNtu2TAGTH6JZ3160/jxFaKyz', 4),
(27, 'doctor3@epacient.al', 'ey3cnZBdMfZsCKB1zIWHo4vXBhakx1QVmdbZ/55amx/cWNyj', 4),
(28, 'doctor4@epacient.al', 'SmkmHpLwCx2FoX+vDTqZmc7XQ8rYTw1/tpq9FzVmT2BftWKg', 4),
(29, 'doctor5@epacient.al', 'uh92sNH2kudGfr0RAc8x9v4lJH6LMKOuANDqrnFWLjdT29TS', 4);

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
  `employee` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `working_hours`
--

INSERT INTO `working_hours` (`id`, `monday_start_time`, `monday_end_time`, `tuesday_start_time`, `tuesday_end_time`, `wednesday_start_time`, `wednesday_end_time`, `thursday_start_time`, `thursday_end_time`, `friday_start_time`, `friday_end_time`, `saturday_start_time`, `saturday_end_time`, `sunday_start_time`, `sunday_end_time`, `employee`) VALUES
(5, '2019-01-23 02:00:00', '2019-01-23 10:00:00', '2019-01-23 20:00:00', '2019-01-23 04:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', '2019-01-23 00:00:00', 3),
(6, '2019-02-01 08:00:00', '2019-02-01 16:00:00', '2019-02-01 08:00:00', '2019-02-01 16:00:00', '2019-02-01 08:00:00', '2019-02-01 16:00:00', '2019-02-01 08:00:00', '2019-02-01 16:00:00', '2019-02-01 08:00:00', '2019-02-01 16:00:00', '2019-02-01 08:00:00', '2019-02-01 16:00:00', '2019-02-01 08:00:00', '2019-02-01 16:00:00', 2);

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
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

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
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

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
