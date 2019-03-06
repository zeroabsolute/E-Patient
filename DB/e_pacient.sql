-- phpMyAdmin SQL Dump
-- version 4.8.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 07, 2019 at 12:28 AM
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
(3, 4, 3, 1),
(4, 4, 1, 1);

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
(9, 'Analizë 1', 'Analizë', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra\\Detyra - EPacient\\Kërkesa Funksionale ePacient.pdf', '2019-03-06 23:58:56', 4, 1),
(10, 'Grafi 1', 'Grafi', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra\\Detyra - EPacient\\Kartela.pdf', '2019-03-06 23:59:11', 4, 1),
(11, 'Test', 'Tjetër', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra\\Detyra - EPacient\\Rezervim 18.pdf', '2019-03-07 00:04:29', 4, 1),
(12, 'Analizë 2', 'Analizë', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra\\Detyra - EPacient\\Kartela.pdf', '2019-03-07 00:26:25', 4, 1),
(13, 'Analizë 1', 'Analizë', 'C:\\Users\\Kev-PC\\Desktop\\POO2\\Detyra\\Detyra - EPacient\\Rezervim.pdf', '2019-03-07 00:27:09', 5, 1);

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
(18, 20, 2, 1),
(19, 21, 3, 1),
(20, 22, 6, 1),
(21, 23, 4, 1),
(22, 24, 1, 1),
(23, 25, 1, 1);

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
(439, 23, '2019-03-01', 1),
(440, 22, '2019-03-02', 1),
(441, 23, '2019-03-03', 1),
(442, 22, '2019-03-04', 1),
(443, 23, '2019-03-05', 1),
(444, 22, '2019-03-06', 1),
(445, 23, '2019-03-07', 1),
(446, 22, '2019-03-08', 1),
(447, 23, '2019-03-09', 1),
(448, 22, '2019-03-10', 1),
(449, 23, '2019-03-11', 1),
(450, 22, '2019-03-12', 1),
(451, 23, '2019-03-13', 1),
(452, 22, '2019-03-14', 1),
(453, 23, '2019-03-15', 1),
(454, 22, '2019-03-16', 1),
(455, 23, '2019-03-17', 1),
(456, 22, '2019-03-18', 1),
(457, 23, '2019-03-19', 1),
(458, 22, '2019-03-20', 1),
(459, 23, '2019-03-21', 1),
(460, 22, '2019-03-22', 1),
(461, 23, '2019-03-23', 1),
(462, 22, '2019-03-24', 1),
(463, 23, '2019-03-25', 1),
(464, 22, '2019-03-26', 1),
(465, 23, '2019-03-27', 1),
(466, 22, '2019-03-28', 1),
(467, 23, '2019-03-29', 1),
(468, 22, '2019-03-30', 1),
(469, 23, '2019-03-31', 1),
(470, 22, '2019-04-01', 1),
(471, 23, '2019-04-02', 1),
(472, 22, '2019-04-03', 1),
(473, 23, '2019-04-04', 1),
(474, 22, '2019-04-05', 1),
(475, 23, '2019-04-06', 1),
(476, 22, '2019-04-07', 1),
(477, 23, '2019-04-08', 1),
(478, 22, '2019-04-09', 1),
(479, 23, '2019-04-10', 1),
(480, 22, '2019-04-11', 1),
(481, 23, '2019-04-12', 1),
(482, 22, '2019-04-13', 1),
(483, 23, '2019-04-14', 1),
(484, 22, '2019-04-15', 1),
(485, 23, '2019-04-16', 1),
(486, 22, '2019-04-17', 1),
(487, 23, '2019-04-18', 1),
(488, 22, '2019-04-19', 1),
(489, 23, '2019-04-20', 1),
(490, 22, '2019-04-21', 1),
(491, 23, '2019-04-22', 1),
(492, 22, '2019-04-23', 1),
(493, 23, '2019-04-24', 1),
(494, 22, '2019-04-25', 1),
(495, 23, '2019-04-26', 1),
(496, 22, '2019-04-27', 1),
(497, 23, '2019-04-28', 1),
(498, 22, '2019-04-29', 1),
(499, 23, '2019-04-30', 1),
(500, 18, '2019-03-01', 1),
(501, 18, '2019-03-02', 1),
(502, 18, '2019-03-03', 1),
(503, 18, '2019-03-04', 1),
(504, 18, '2019-03-05', 1),
(505, 18, '2019-03-06', 1),
(506, 18, '2019-03-07', 1),
(507, 18, '2019-03-08', 1),
(508, 18, '2019-03-09', 1),
(509, 18, '2019-03-10', 1),
(510, 18, '2019-03-11', 1),
(511, 18, '2019-03-12', 1),
(512, 18, '2019-03-13', 1),
(513, 18, '2019-03-14', 1),
(514, 18, '2019-03-15', 1),
(515, 18, '2019-03-16', 1),
(516, 18, '2019-03-17', 1),
(517, 18, '2019-03-18', 1),
(518, 18, '2019-03-19', 1),
(519, 18, '2019-03-20', 1),
(520, 18, '2019-03-21', 1),
(521, 18, '2019-03-22', 1),
(522, 18, '2019-03-23', 1),
(523, 18, '2019-03-24', 1),
(524, 18, '2019-03-25', 1),
(525, 18, '2019-03-26', 1),
(526, 18, '2019-03-27', 1),
(527, 18, '2019-03-28', 1),
(528, 18, '2019-03-29', 1),
(529, 18, '2019-03-30', 1),
(530, 18, '2019-03-31', 1);

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
(18, 'Nurse', '1', '0123456789', 'Rruga e Elbasanit', '1988-03-06', 34, 1),
(19, 'Nurse', '2', '0123456789', 'Rruga e Elbasanit', '1988-03-06', 35, 1),
(20, 'Doctor', '1', '0123456789', 'Rruga e Kavajës', '1970-03-06', 36, 1),
(21, 'Doctor', '2', '0123456789', 'Rruga e Kavajës', '1945-03-06', 37, 1),
(22, 'Doctor', '3', '0123456789', 'Rruga e Durrësit', '1955-03-06', 38, 1),
(23, 'Doctor', '4', '0123456789', 'Rruga e Barrikadave', '1961-03-06', 39, 1),
(24, 'Doctor', '5', '0123456789', 'Rruga e Dibrës', '1977-03-06', 40, 1),
(25, 'Doctor', '6', '0123456789', 'Rruga Naim Frashëri', '1981-03-06', 41, 1);

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
(2, 'Manager', '1', 31, 1);

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
(2, 'Ibuprofen', 'Anti-Inflamator', '2024-01-23', 'Përbërës 1, Përbërës 2, Përbërës 3', 0),
(3, 'Vitamina B', 'Tableta', '2023-03-06', 'Vitamina natyrore 10%, Helm 20%', 1);

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
(4, 18, 1),
(5, 19, 1);

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
(5, 'Operator', '1', '1984-03-06', 32, 1),
(6, 'Operator', '2', '1988-03-06', 33, 1);

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
(4, 'Pacient', '1', '2002-03-06', 'Mashkull', '0192837465', 'Rruga e Durrësit', 1),
(5, 'Pacient', '2', '1989-03-06', 'Femër', '0384756918', 'Rruga Lidhja e Prizrenit', 1);

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
(4, '2019-03-06 00:00:00', 4, 1),
(5, '2019-03-06 00:00:00', 5, 1);

-- --------------------------------------------------------

--
-- Table structure for table `prescription`
--

CREATE TABLE `prescription` (
  `id` int(11) NOT NULL,
  `description` text NOT NULL,
  `reservation` int(11) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `prescription`
--

INSERT INTO `prescription` (`id`, `description`, `reservation`, `status`) VALUES
(11, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas at molestie velit. Praesent vestibulum sodales pretium. Ut feugiat consectetur risus in malesuada.', 4, 1);

-- --------------------------------------------------------

--
-- Table structure for table `prescription_medicament`
--

CREATE TABLE `prescription_medicament` (
  `id` int(11) NOT NULL,
  `prescription` int(11) NOT NULL,
  `medicament` int(11) NOT NULL,
  `status` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `prescription_medicament`
--

INSERT INTO `prescription_medicament` (`id`, `prescription`, `medicament`, `status`) VALUES
(9, 11, 2, 1);

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

--
-- Dumping data for table `reservation`
--

INSERT INTO `reservation` (`id`, `start_datetime`, `end_datetime`, `service`, `created_by`, `patient`, `nurse`, `doctor`, `status`) VALUES
(4, '2019-03-07 08:12:00', '2019-03-07 09:12:00', 3, 5, 4, 4, 18, 1),
(5, '2019-03-07 09:16:00', '2019-03-07 09:32:00', 1, 5, 5, 4, 18, 1),
(6, '2019-03-07 09:16:00', '2019-03-07 09:50:00', 2, 5, 4, 5, 19, 1);

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
(2, 'Analiza të mushkërive', 3500, 'Konsultë me specialistin, grafi', 0),
(3, 'Analiza gjaku', 1200, 'Analizë e plotë gjaku', 1);

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
(31, 'manager1@epacient.al', '9wlTxkC2Xna37ZxFmbxNljwY4g/sTn24S+FlB4zs7UjUUns+', 2, 1),
(32, 'operator1@epacient.al', 'vMedS3wndC2hEvjMrRsg5HTbiMHxHWVO7AONz8KnCfxiIUnU', 1, 1),
(33, 'operator2@epacient.al', 'ME5CoITe74P4I5HoJe96LFxfl48Fq91Rl+D0BLLvz8/MaADH', 1, 0),
(34, 'nurse1@epacient.al', 'wRlfJBT1/wKM5yBN9NtKtKXGlWXyefKMf4Q+3ADkrLhuSJJs', 3, 1),
(35, 'nurse2@epacient.al', 'mByYz3hkMLH41wmLQ2V5hPo5jVxsVs9hzpJgWVcLHWNkJuRt', 3, 1),
(36, 'doctor1@epacient.al', 'yMp1yqF3zDToVWXc271roMLJ8MZEVGXFLy3E7gByWYI40NpH', 4, 1),
(37, 'doctor2@epacient.al', 'wrdxxMzqFWTvF3xxyspx6xt8KV6CS4ycVTPMmY7cu7OB8Ei4', 4, 1),
(38, 'doctor3@epacient.al', 'DjjuL99JNW9US725a61J/xd1pG6SYPfIJK+pAnnKGuC/VB2K', 4, 1),
(39, 'doctor4@epacient.al', 'EgEweczQ9rH20km8qp8/eCbalNrcdC1K+vmQxy4uQlREOdrr', 4, 1),
(40, 'doctor5@epacient.al', '/kVMUB5i68H6bVD7c4V34WgHbzi/9NQ7CXFD9A4P7woPXMsy', 4, 1),
(41, 'doctor6@epacient.al', 'aIoW3IZnlqaN6iLUWWMaNlnEvSu1h9TcxyFwvzMu/fT1frki', 4, 1);

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
(8, '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 17:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', 18, 1),
(9, '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 17:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', 19, 1),
(10, '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 17:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', 20, 1),
(11, '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 17:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', 21, 1),
(12, '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 17:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', 22, 1),
(13, '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 17:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', 23, 1),
(14, '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 17:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', 24, 1),
(15, '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 17:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', '2019-03-06 08:00:00', '2019-03-06 16:00:00', 25, 1);

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
-- Indexes for table `prescription`
--
ALTER TABLE `prescription`
  ADD PRIMARY KEY (`id`),
  ADD KEY `reservation` (`reservation`);

--
-- Indexes for table `prescription_medicament`
--
ALTER TABLE `prescription_medicament`
  ADD PRIMARY KEY (`id`),
  ADD KEY `medicament` (`medicament`),
  ADD KEY `recipt` (`prescription`);

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
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `chart_document`
--
ALTER TABLE `chart_document`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `doctor`
--
ALTER TABLE `doctor`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT for table `emergency_doctor`
--
ALTER TABLE `emergency_doctor`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=531;

--
-- AUTO_INCREMENT for table `employee`
--
ALTER TABLE `employee`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

--
-- AUTO_INCREMENT for table `manager`
--
ALTER TABLE `manager`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `medicament`
--
ALTER TABLE `medicament`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `nurse`
--
ALTER TABLE `nurse`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `operator`
--
ALTER TABLE `operator`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `patient`
--
ALTER TABLE `patient`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `patient_chart`
--
ALTER TABLE `patient_chart`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `prescription`
--
ALTER TABLE `prescription`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `prescription_medicament`
--
ALTER TABLE `prescription_medicament`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `reservation`
--
ALTER TABLE `reservation`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

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
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=42;

--
-- AUTO_INCREMENT for table `working_hours`
--
ALTER TABLE `working_hours`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

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
-- Constraints for table `prescription`
--
ALTER TABLE `prescription`
  ADD CONSTRAINT `prescription_ibfk_1` FOREIGN KEY (`reservation`) REFERENCES `reservation` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `prescription_medicament`
--
ALTER TABLE `prescription_medicament`
  ADD CONSTRAINT `prescription_medicament_ibfk_1` FOREIGN KEY (`medicament`) REFERENCES `medicament` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `prescription_medicament_ibfk_2` FOREIGN KEY (`prescription`) REFERENCES `prescription` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

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
