import {
  Button,
  Flex,
  VStack,
  Text,
  Portal,
  Popover,
  IconButton,
  Input,
  Spinner,
} from '@chakra-ui/react';
import { LuPlus } from 'react-icons/lu';
import { IoPaperPlane } from 'react-icons/io5';
import { useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { useMutation, useQuery, useQueryClient } from 'react-query';
import { toaster } from '@/components/ui/toaster';
import { addStudentToClass, getClasses } from '@/shared/api/classesApi';
import { getUserById, User } from '@/shared/api/userApi';
import { Avatar } from '@/components/ui/avatar';

const ClassStudents = () => {
  const { classId } = useParams<{ classId: string }>();
  const [email, setEmail] = useState('');
  const queryClient = useQueryClient();
  const [studentUsers, setStudentUsers] = useState<User[]>([]);
  const [loadingUsers, setLoadingUsers] = useState(true);
  const teacherId = Number(localStorage.getItem('userId'));
  const { data: classes } = useQuery(['classes', teacherId], () => getClasses(teacherId));

  const { mutate: inviteStudent } = useMutation({
    mutationFn: () => addStudentToClass(email, Number(classId)),
    onSuccess: () => {
      toaster.create({
        title: `Учня запрошено успішно!`,
        type: 'success',
      });
      queryClient.invalidateQueries(['classes']);
      setEmail('');
    },
    onError: (err: any) => {
      toaster.create({
        title: `Помилка при запрошенні учня ${err}`,
        type: 'error',
      });
    },
  });

  useEffect(() => {
    const loadStudents = async () => {
      setLoadingUsers(true);
      try {
        const currentClass = classes?.find((cls) => cls.id === Number(classId));
        if (!currentClass || currentClass.studentsIds.length === 0) {
          setStudentUsers([]);
          return;
        }

        const studentPromises = currentClass.studentsIds.map((id: number) => getUserById(id));
        const resolvedUsers = await Promise.all(studentPromises);
        setStudentUsers(resolvedUsers);
      } catch (err) {
        toaster.error({ title: `Не вдалося завантажити учнів: ${err}` });
      } finally {
        setLoadingUsers(false);
      }
    };

    if (classes && classId) {
      loadStudents();
    }
  }, [classes, classId]);

  return (
    <>
      <Popover.Root>
        <Popover.Trigger asChild>
          <Button borderRadius="1rem" colorPalette="orange">
            <LuPlus /> Додати учня
          </Button>
        </Popover.Trigger>
        <Portal>
          <Popover.Positioner>
            <Popover.Content css={{ '--popover-bg': 'white' }}>
              <Popover.Arrow />
              <Popover.Body>
                <Popover.Title fontWeight="medium" color="black" fontSize="md">
                  Запросити учня
                </Popover.Title>
                <Flex flexDir="row" justifyContent="center" alignItems="center" mt={4}>
                  <Input
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    colorPalette="orange"
                    borderRadius="1rem"
                    placeholder="Введіть електронну пошту"
                    size="md"
                    color="black"
                  />
                  <IconButton
                    onClick={() => inviteStudent()}
                    borderRadius="1rem"
                    aria-label="Invite user"
                    variant="ghost"
                  >
                    <IoPaperPlane color="#f97316" size={28} />
                  </IconButton>
                </Flex>
              </Popover.Body>
            </Popover.Content>
          </Popover.Positioner>
        </Portal>
      </Popover.Root>
      {loadingUsers ? (
        <Spinner mt={6} />
      ) : studentUsers.length === 0 ? (
        <Text mt={6} color="gray.500">
          У класі поки немає учнів
        </Text>
      ) : (
        <Flex overflowY="auto" flexDir="column" p={2} height="calc(100vh - 260px)">
          <VStack gap={4} mt={6}>
            {studentUsers.map((user) => (
              <Flex
                key={user.id}
                borderRadius="1rem"
                w="full"
                boxShadow="sm"
                p="1rem"
                flexDir="row"
                justifyContent="space-between"
                alignItems="center"
              >
                <Flex gap={4} flexDir="row" alignItems="center">
                  <Avatar name={`${user.firstName} ${user.secondName}`} />
                  <Text fontSize="md">{`${user.secondName} ${user.firstName}`}</Text>
                </Flex>
              </Flex>
            ))}
          </VStack>
        </Flex>
      )}
    </>
  );
};

export default ClassStudents;
