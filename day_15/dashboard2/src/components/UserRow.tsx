import React from 'react';
import { TableCell, Checkbox, Avatar, Box, Typography, IconButton } from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';

interface User {
  name: { first: string; last: string };
  email: string;
  phone: string;
  picture: { thumbnail: string };
  location: { street: { number: number; name: string }; city: string; state: string; postcode: string };
  dob: { date: string; age: number };
  registered: { date: string };
}

interface UserRowProps {
  user: User;
}

const UserRow: React.FC<UserRowProps> = ({ user }) => {
  return (
    <>
      <TableCell><Checkbox /></TableCell>
      <TableCell>
        <Box sx={{ display: 'flex', alignItems: 'center' }}>
          <Avatar src={user.picture.thumbnail} alt={`${user.name.first} ${user.name.last}`} sx={{ mr: 1, width: 32, height: 32 }} />
          <Typography variant="body2">{`${user.name.first} ${user.name.last}`}</Typography>
        </Box>
      </TableCell>
      <TableCell>{user.email}</TableCell>
      <TableCell>{user.phone}</TableCell>
      <TableCell>
        <IconButton size="small">
          <EditIcon fontSize="small" />
        </IconButton>
      </TableCell>
    </>
  );
};

export default UserRow;