import { Box } from "@mui/material";

export interface TabPanelProps {
  children?: React.ReactNode;
  index: number;
  value: number;
}

export const TabPanel = ({ children, value, index }: TabPanelProps): JSX.Element => {
  return (
    <Box role="tabpanel" id={`tabpanel-${index}`}>
      {value === index && <Box>{children}</Box>}
    </Box>
  );
};
