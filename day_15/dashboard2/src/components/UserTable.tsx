import React, { useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import { Table, TableBody, TableCell, TableHead, TableRow, Checkbox, Skeleton, Pagination, Box, Collapse, IconButton, Typography } from '@mui/material';
import { motion } from 'framer-motion';
import UserRow from './UserRow';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import ExpandLessIcon from '@mui/icons-material/ExpandLess';

interface User {
  name: { first: string; last: string };
  email: string;
  phone: string;
  picture: { thumbnail: string };
  location: { street: { number: number; name: string }; city: string; state: string; postcode: string };
  dob: { date: string; age: number };
  registered: { date: string };
}

interface UserResponse {
  results: User[];
}

const fetchUsers = async (page: number): Promise<User[]> => {
  const res = await fetch(`https://randomuser.me/api?page=${page}&results=10`);
  const data: UserResponse = await res.json();
  return data.results;
};

const UserTable: React.FC = () => {
  const [page, setPage] = useState(1);
  const [expandedRow, setExpandedRow] = useState<string | null>(null);
  const itemsPerPage = 10;
  const totalPages = 48;

  const { data, isLoading, error } = useQuery<User[]>({
    queryKey: ['users', page],
    queryFn: () => fetchUsers(page),
    placeholderData: (previousData) => previousData,
    refetchOnWindowFocus: false,
    refetchOnMount: false,
    refetchInterval: false,
  });

  const handlePageChange = (event: React.ChangeEvent<unknown>, value: number) => {
    setPage(value);
  };

  const handleExpand = (email: string) => {
    setExpandedRow(expandedRow === email ? null : email);
  };

  const getHRYear = (registeredDate: string): string => {
    const regDate = new Date(registeredDate);
    const years = new Date().getFullYear() - regDate.getFullYear();
    return `${years} Years`;
  };

  if (error) return <div>An error occurred</div>;

  return (
    <Box sx={{ backgroundColor: '#fff', borderRadius: 8, overflow: 'hidden', boxShadow: '0 2px 4px rgba(0,0,0,0.1)' }}>
      <Box sx={{ overflowX: 'auto' }}>
        <Table sx={{ minWidth: { xs: 650, sm: 800 } }}>
          <TableHead>
            <TableRow>
              <TableCell />
              <TableCell><Checkbox /></TableCell>
              <TableCell>Name</TableCell>
              <TableCell sx={{ display: { xs: 'none', sm: 'table-cell' } }}>Email</TableCell>
              <TableCell sx={{ display: { xs: 'none', sm: 'table-cell' } }}>Phone</TableCell>

            </TableRow>
          </TableHead>
          <TableBody>
            {isLoading ? (
              Array(itemsPerPage).fill(0).map((_, index) => (
                <TableRow key={index}>
                  <TableCell />
                  <TableCell><Skeleton variant="rectangular" width={20} height={20} /></TableCell>
                  <TableCell><Skeleton variant="text" /></TableCell>
                  <TableCell sx={{ display: { xs: 'none', sm: 'table-cell' } }}><Skeleton variant="text" /></TableCell>
                  <TableCell sx={{ display: { xs: 'none', sm: 'table-cell' } }}><Skeleton variant="text" /></TableCell>
                  <TableCell><Skeleton variant="text" /></TableCell>
                  <TableCell><Skeleton variant="text" /></TableCell>
                  <TableCell><Skeleton variant="text" /></TableCell>
                </TableRow>
              ))
            ) : (
              data?.map((user) => (
                <React.Fragment key={user.email}>
                  <TableRow
                    component={motion.tr}
                    initial={{ opacity: 0 }}
                    animate={{ opacity: 1 }}
                    transition={{ duration: 0.3 }}
                  >
                    <TableCell>
                      <IconButton onClick={() => handleExpand(user.email)}>
                        {expandedRow === user.email ? <ExpandLessIcon /> : <ExpandMoreIcon />}
                      </IconButton>
                    </TableCell>
                    <UserRow user={user} />

                  </TableRow>
                  <TableRow>
                    <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={8}>
                      <Collapse in={expandedRow === user.email} timeout="auto" unmountOnExit>
                        <Box sx={{ margin: 1 }}>
                          
                          
                          <Typography variant="body2" sx={{ fontSize: { xs: '0.75rem', sm: '0.875rem' } }}>
                            Age: {user.dob.age}
                          </Typography>
                          
                          <Typography variant="body2" sx={{ fontSize: { xs: '0.75rem', sm: '0.875rem' } }}>
                            Address: {user.location.street.number} {user.location.street.name}, {user.location.city}, {user.location.state} {user.location.postcode}
                          </Typography>
                        </Box>
                      </Collapse>
                    </TableCell>
                  </TableRow>
                </React.Fragment>
              ))
            )}
          </TableBody>
        </Table>
      </Box>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', p: { xs: 1, sm: 2 }, backgroundColor: '#fff', borderBottomLeftRadius: 8, borderBottomRightRadius: 8 }}>
        <Typography variant="body2" sx={{ fontSize: { xs: '0.75rem', sm: '0.875rem' } }}>
          {page} of {totalPages}
        </Typography>
        <Pagination
          count={totalPages}
          page={page}
          onChange={handlePageChange}
          color="primary"
          size="small"
          sx={{ '& .MuiPaginationItem-root': { fontSize: { xs: '0.75rem', sm: '1rem' } } }}
        />
      </Box>
    </Box>
  );
};

export default UserTable;