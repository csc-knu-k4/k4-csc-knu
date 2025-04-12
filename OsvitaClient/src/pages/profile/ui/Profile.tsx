import { UserAvatar } from '@/shared/ui/Avatar';
import { Badge, Button, Flex, Text, Container } from '@chakra-ui/react';
import { CiCalendar } from 'react-icons/ci';
import { FaArrowTrendUp } from 'react-icons/fa6';
import { PiMedal } from 'react-icons/pi';
import { FaRegCheckCircle } from 'react-icons/fa';
import { Avatar } from '@/components/ui/avatar';
const Profile = () => {
  return (
    <Container centerContent>
      <Flex flexDir="row" flexWrap="wrap" gap={8}>
        <Flex flexDir="column" justifyContent="center" alignItems="center" gap={8}>
          <Flex
            borderRadius="1rem"
            boxShadow="sm"
            flexDir="column"
            justifyContent="center"
            alignItems="center"
            h="270px"
            w="550px"
          >
            <Avatar size="2xl" />
            <Text fontSize="xl" my={4} fontWeight="bold">
              Костюк Світлана
            </Text>
            <Badge colorPalette="gray">Учень</Badge>
          </Flex>
          <Flex
            p="25px 50px"
            h="150px"
            w="550px"
            borderRadius="1rem"
            boxShadow="sm"
            flexDir="column"
          >
            <Button
              mb={6}
              borderRadius="1rem"
              fontSize="lg"
              fontWeight="semibold"
              colorPalette="orange"
            >
              <CiCalendar size="1.5rem" />
              Щоденні Завдання
            </Button>
            <Flex flexDir="row" justifyContent="space-between" alignItems="center">
              <Flex gap={1} flexDir="row" alignItems="center" justifyContent="center" mb={5}>
                <FaRegCheckCircle size="1.5rem" color="green" />
                <Text color="gray">Сьогоднішнє завдання виконане</Text>
              </Flex>
              <Flex gap={1} flexDir="row" alignItems="center" justifyContent="center" mb={5}>
                <PiMedal size="1.5rem" color="rgb(255, 109, 24)" />
                <Text fontSize="lg" fontWeight="semibold" color="orange">
                  345 балів
                </Text>
              </Flex>
            </Flex>
          </Flex>
        </Flex>
        <Flex
          p="20px 50px"
          borderRadius="1rem"
          boxShadow="sm"
          fontSize="xl"
          fontWeight="semibold"
          h="450px"
          w="550px"
          flexDir="column"
        >
          <Flex gap={2} flexDir="row" alignItems="center" justifyContent="center" mb={5}>
            <FaArrowTrendUp color="rgb(255, 109, 24)" />
            <Text color="orange">Рейтинг</Text>
          </Flex>
          <Flex gap={2} flexDir="column" overflowY="auto" h="200px">
            <Flex
              borderRadius="1rem"
              borderColor="orange"
              borderWidth="1px"
              flexDir="row"
              flexWrap="nowrap"
              justifyContent="space-between"
              alignItems="center"
              h="45px"
              px={4}
            >
              <Flex flexDir="row" alignItems="center" gap={2}>
                <Text color="orange.200" fontSize="lg">
                  1
                </Text>
                <UserAvatar />
                <Text color="orange.200" fontSize="lg">
                  Костюк Світлана
                </Text>
              </Flex>
              <Flex gap={1} flexDir="row" alignItems="center" justifyContent="center">
                <Text fontSize="md" fontWeight="semibold" color="orange.200">
                  345 балів
                </Text>
                <PiMedal color="rgb(255, 109, 24)" />
              </Flex>
            </Flex>

            <Flex
              borderRadius="1rem"
              borderColor="orange"
              borderWidth="1px"
              flexDir="row"
              flexWrap="nowrap"
              justifyContent="space-between"
              alignItems="center"
              h="45px"
              px={4}
            >
              <Flex flexDir="row" alignItems="center" gap={2}>
                <Text color="orange.200" fontSize="lg">
                  2
                </Text>
                <UserAvatar />
                <Text color="orange.200" fontSize="lg">
                  Костюк Світлана
                </Text>
              </Flex>
              <Flex gap={1} flexDir="row" alignItems="center" justifyContent="center">
                <Text fontSize="md" fontWeight="semibold" color="orange.200">
                  345 балів
                </Text>
                <PiMedal color="rgb(255, 109, 24)" />
              </Flex>
            </Flex>

            <Flex
              borderRadius="1rem"
              borderColor="orange"
              borderWidth="1px"
              flexDir="row"
              flexWrap="nowrap"
              justifyContent="space-between"
              alignItems="center"
              h="45px"
              px={4}
            >
              <Flex flexDir="row" alignItems="center" gap={2}>
                <Text color="orange.200" fontSize="lg">
                  3
                </Text>
                <UserAvatar />
                <Text color="orange.200" fontSize="lg">
                  Костюк Світлана
                </Text>
              </Flex>
              <Flex gap={1} flexDir="row" alignItems="center" justifyContent="center">
                <Text fontSize="md" fontWeight="semibold" color="orange.200">
                  345 балів
                </Text>
                <PiMedal color="rgb(255, 109, 24)" />
              </Flex>
            </Flex>

            <Flex
              borderRadius="1rem"
              borderColor="orange"
              borderWidth="1px"
              flexDir="row"
              flexWrap="nowrap"
              justifyContent="space-between"
              alignItems="center"
              h="45px"
              px={4}
            >
              <Flex flexDir="row" alignItems="center" gap={2}>
                <Text color="orange.200" fontSize="lg">
                  4
                </Text>
                <UserAvatar />
                <Text color="orange.200" fontSize="lg">
                  Костюк Світлана
                </Text>
              </Flex>
              <Flex gap={1} flexDir="row" alignItems="center" justifyContent="center">
                <Text fontSize="md" fontWeight="semibold" color="orange.200">
                  345 балів
                </Text>
                <PiMedal color="rgb(255, 109, 24)" />
              </Flex>
            </Flex>

            <Flex
              borderRadius="1rem"
              borderColor="orange"
              borderWidth="1px"
              flexDir="row"
              flexWrap="nowrap"
              justifyContent="space-between"
              alignItems="center"
              h="45px"
              px={4}
            >
              <Flex flexDir="row" alignItems="center" gap={2}>
                <Text color="orange.200" fontSize="lg">
                  5
                </Text>
                <UserAvatar />
                <Text color="orange.200" fontSize="lg">
                  Костюк Світлана
                </Text>
              </Flex>
              <Flex gap={1} flexDir="row" alignItems="center" justifyContent="center">
                <Text fontSize="md" fontWeight="semibold" color="orange.200">
                  345 балів
                </Text>
                <PiMedal color="rgb(255, 109, 24)" />
              </Flex>
            </Flex>
          </Flex>
        </Flex>
      </Flex>
    </Container>
  );
};

export default Profile;
