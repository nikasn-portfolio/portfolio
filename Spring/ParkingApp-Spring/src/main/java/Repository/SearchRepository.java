package Repository;

import org.springframework.data.jpa.repository.JpaSpecificationExecutor;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.NoRepositoryBean;
import org.springframework.data.repository.Repository;
import org.springframework.data.repository.query.Param;


import java.io.Serializable;
import java.util.Optional;
import java.util.UUID;

@NoRepositoryBean
public interface SearchRepository<T, ID extends Serializable> extends JpaSpecificationExecutor<T> {
    @Query("select t from #{#entityName} t")
    Optional<T> findOne();
}
