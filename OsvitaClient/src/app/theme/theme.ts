import { createSystem, defaultConfig } from "@chakra-ui/react";

const theme = createSystem(defaultConfig, {
	globalCss: {
		body: {
			margin: 0,
			padding: "1.25rem 1.5rem",
			bgColor: "gray.100",
			overflow: "hidden",
		},
	},
});

export default theme;
