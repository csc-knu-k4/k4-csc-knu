import { ChakraProvider } from "@chakra-ui/react";
import { ThemeProvider } from "next-themes";
import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import BaseLayout from "@/app/layouts/BaseLayout";
import theme from "@/app/theme/theme";

createRoot(document.getElementById("root")!).render(
	<StrictMode>
		<ChakraProvider value={theme}>
			<ThemeProvider attribute="class" disableTransitionOnChange>
				<BaseLayout />
			</ThemeProvider>
		</ChakraProvider>
	</StrictMode>
);
