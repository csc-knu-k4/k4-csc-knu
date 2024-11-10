import { Grid, GridItem, Box, Button, Input, Flex } from "@chakra-ui/react";
import { Outlet } from "react-router-dom";

export default function BaseLayout() {
	return (
		<Grid
			templateAreas={`"sidebar toolbar" "sidebar content"`}
			templateColumns="16rem 1fr"
			templateRows="auto 1fr"
			gap="1.5rem"
			h="100vh"
		>
			{/* Боковое меню */}
			<GridItem area="sidebar" h="calc(100vh - 2.5rem)">
				<Flex flexDir="column" borderRadius="1rem" bg="white" h="full">
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
			</GridItem>

			{/* Панель инструментов */}
			<GridItem area="toolbar">
				<Box bg="white" p={4} borderRadius="1rem" w="full">
					<Input size="sm" />
				</Box>
			</GridItem>

			{/* Основной контент */}
			<GridItem area="content" overflowY="auto">
				<Box bg="white" p={5} borderRadius="1rem" h="calc(100vh - 8.25rem)">
					<Outlet />
				</Box>
			</GridItem>
		</Grid>
	);
}
