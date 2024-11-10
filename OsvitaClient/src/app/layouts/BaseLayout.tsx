import { Flex, Box, Button, Input } from "@chakra-ui/react";
import { Outlet } from "react-router-dom";

export default function BaseLayout() {
	return (
		<Flex h="100vh">
			<Box
				borderRadius="1rem"
				w="16rem"
				bg="white"
				mr="1.5rem"
				position="fixed"
				h="calc(100vh - 2.5rem)"
			>
				<Flex flexDir="column">
					<Button borderRadius="none" bgColor="#E4E7FF" color="#5C6CFF">
						Предмети
					</Button>
					<Button borderRadius="none" bgColor="#E4E7FF" color="#5C6CFF">
						Розділи
					</Button>
					<Button borderRadius="none" bgColor="#E4E7FF" color="#5C6CFF">
						Теми
					</Button>
					<Button borderRadius="none" bgColor="#E4E7FF" color="#5C6CFF">
						Матеріали
					</Button>
				</Flex>
			</Box>

			<Flex flexDir="column" flex="1" ml="17.5rem" gap="1.5rem">
				<Box
					w="calc(100% - 20.5rem)"
					h="4rem"
					bg="white"
					p={4}
					borderRadius="1rem"
					position="fixed"
					top="1.25rem"
					right="1.5rem"
					zIndex="10"
				>
					<Input size="sm" />
				</Box>

				<Box
					mt={5}
					p={5}
					bg="white"
					borderRadius="1rem"
					h="calc(100vh - 7.5rem)"
					overflowY="auto"
					position="fixed"
					top="5rem"
					left="19rem"
					right="1.5rem"
				>
					<Outlet />
				</Box>
			</Flex>
		</Flex>
	);
}
