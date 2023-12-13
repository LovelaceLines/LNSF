import { ListItemButton, ListItemIcon, ListItemText, Collapse, List, Icon } from '@mui/material';
import { ExpandLess, ExpandMore } from "@mui/icons-material";
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';

interface IItemOption {
  pathOption: string;
  labelOption: string;
}

interface IListComponentProps {
  index: number;
  icon: string;
  maintitle: string;
  subItems: IItemOption[];
  openIndex: number;
  onClickCollapse: (index: number) => void;
}

export const ListUlMenu: React.FC<IListComponentProps> = ({ index, icon, maintitle, subItems, openIndex, onClickCollapse }) => {

  const navigate = useNavigate();
  const [selectedSubItem, setSelectedSubItem] = useState<string | null>(null);

  const handleClick = (to: string) => {
    setSelectedSubItem(to);
    navigate(to);
  }

  return (
    <>
      <ListItemButton onClick={() => onClickCollapse(index)}>
        <ListItemIcon>
          <Icon color='primary'>{icon}</Icon>
        </ListItemIcon>
        <ListItemText primary={maintitle} />
        {openIndex === index ? <ExpandLess /> : <ExpandMore />}
      </ListItemButton>


      <Collapse in={openIndex === index} timeout="auto" unmountOnExit>
        <List component="div" disablePadding>
          {subItems.map((subItem, subItemIndex) => (
            <ListItemButton
              key={subItemIndex}
              selected={selectedSubItem === subItem.pathOption}
              onClick={() => { handleClick(subItem.pathOption) }}
              sx={{ pl: 9 }}
            >
              <ListItemText primary={subItem.labelOption} />
            </ListItemButton>
          ))}
        </List>
      </Collapse>
    </>
  );
};